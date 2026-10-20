using Neuvolics;
using Quintessential;
using NeuvolicsUtil;

namespace NeuvolicsParts;

internal static class ConsolidationGlyph {

    public static readonly HexIndex ConsolidationHole1Hex = new(0, 1);
    public static readonly HexIndex ConsolidationHole2Hex = new(1, -1);
    public static readonly HexIndex ConsolidationZephironIrisHex = new(0, 0);

    public static void Render(Part part, Vector2 pos, SolutionEditorBase editor, PartRenderer renderer)
    {
        PartSimState pss = editor.GetSimulation().GetSimState(part);
        IntermediatePartState uco = editor.GetIntermState(part, pos);
        float time = editor.GetCycleTime();

        int atomsPresent = 0;

        HexIndex[] holes = new HexIndex[] { ConsolidationHole1Hex, ConsolidationHole2Hex };
        foreach (HexIndex h in holes)
        {
            foreach (Molecule m in editor.GetSimulation().GetMolecules())
            {
                if (m.GetAtoms().Count == 1 && m.GetAtoms().TryGetValue(part.InFrontBy(h), out Atom a))
                {
                    AtomType aT = a.atomType;
                    if (aT == Atoms.gelaron)
                    {
                        atomsPresent |= 1;
                    }
                    else if (aT == Atoms.frixon)
                    {
                        atomsPresent |= 2;
                    }
                }
            }
        }

        Vector2 pivot = new(41f, 119f);
        Vector2 offset = new(-1f, -1f);
        renderer.RenderBase(Textures.Consolidation.Base, offset, pivot, 0f);

        foreach (HexIndex h in holes)
        {
            renderer.RenderShadowCircle(Textures.Consolidation.VolicInput, h, 0f);
            renderer.RenderUpright(Textures.Consolidation.HoleBar, h, Vector2.Zero);
            renderer.RenderUpright((atomsPresent & 1) == 0 ? Textures.Consolidation.HoleGelaronActive : Textures.Consolidation.HoleGelaronInactive, h, Vector2.Zero);
            renderer.RenderUpright((atomsPresent & 2) == 0 ? Textures.Consolidation.HoleFrixonActive : Textures.Consolidation.HoleFrixonInactive, h, Vector2.Zero);
        }

        // iris
        int irisFrame = 15;
        bool afterIrisOpens = false;
        Molecule risingAtom = null;
        Vector2 risingOffset = uco.pos + HexGrid.standardGrid.ToPixelCoords(ConsolidationZephironIrisHex).Rotated(uco.rotation);

        renderer.RenderRotating(Textures.Consolidation.IrisBase, ConsolidationZephironIrisHex, Vector2.Zero);
        if (pss.isProcessing)
        {
            irisFrame = Utils.Clamp((int)(Utils.InterpolateLinear(1f, -1f, time) * 16f), 0, 15);
            afterIrisOpens = time > 0.5f;
            risingAtom = Molecule.CreateMonoatomic(pss.processingAtoms[0]);
            if (!afterIrisOpens)
            {
                // show atom rising behind iris
                Editor.RenderMolecule(risingAtom, risingOffset, new HexIndex(0, 0), 0f, 1f, time, 1f, false, null);
            }
        }
        renderer.RenderUpright(Textures.Irises.Zephiron[irisFrame], ConsolidationZephironIrisHex, Vector2.Zero);
        renderer.RenderRotating(Textures.Consolidation.IrisLip, ConsolidationZephironIrisHex, Vector2.Zero);
        if (pss.isProcessing && afterIrisOpens)
        {
            // show atom rising infront of iris
            Editor.RenderMolecule(risingAtom, risingOffset, new HexIndex(0, 0), 0f, 1f, time, 1f, false, null);
        }
        renderer.RenderBase(Textures.Consolidation.Connectors, offset, pivot, 0);
    }
    public static bool OnCycle(Sim sim, Part part, PartSimState pss, GlyphRecipe recipe, bool first)
    {
        if (first)
        {
            HexIndex h1 = part.InFrontBy(ConsolidationHole1Hex);
            HexIndex h2 = part.InFrontBy(ConsolidationHole2Hex);
            HexIndex ia = part.InFrontBy(ConsolidationZephironIrisHex);

            if (sim.FindAtom(ia).HasValue())
            {
                // blocked!
                return false;
            }
            if (!sim.FindAtom(h1).GetOrDefault(out AtomReference a1) || a1.inMultiAtomMolecule || a1.isHeldByArm)
            {
                return false;
            }
            if (!sim.FindAtom(h2).GetOrDefault(out AtomReference a2) || a2.inMultiAtomMolecule || a2.isHeldByArm)
            {
                return false;
            }

            if ((a1.atomType != Atoms.frixon || a2.atomType != Atoms.gelaron) && (a1.atomType != Atoms.gelaron || a2.atomType != Atoms.frixon))
            {
                return false;
            }

            a1.molecule.RemoveAtom(a1.pos);
            a2.molecule.RemoveAtom(a2.pos);

            sim.solutionEditor.DrawFallingAtom(a1);
            sim.solutionEditor.DrawFallingAtom(a2);

            sim.SpawnColliderRelative(part, ConsolidationZephironIrisHex);
            pss.isProcessing = true;
            pss.processingAtoms = new AtomType[1] { Atoms.zephiron };

            // Brimstone.API.PlaySound(sim, ConsolidationSound);
            Sounds.glyph_consolidation.PlaySound(sim.solutionEditor.method_506());
            return true;
        }
        else if (pss.isProcessing)
        {
            sim.SpawnAtomRelative(pss.processingAtoms[0], part, ConsolidationZephironIrisHex);
            return true;
        }
        return false;
    }
}
