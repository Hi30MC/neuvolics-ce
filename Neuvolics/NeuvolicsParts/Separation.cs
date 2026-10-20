using Neuvolics;
using Quintessential;
using NeuvolicsUtil;

namespace NeuvolicsParts;

internal static class SeparationGlyph {

    public static readonly HexIndex SeparationHoleHex = new(0, 0);
    public static readonly HexIndex SeparationGelaronIrisHex = new(1, 0);
    public static readonly HexIndex SeparationFrixonIrisHex = new(-1, 0);

    public static void Render(Part part, Vector2 pos, SolutionEditorBase editor, PartRenderer renderer)
    {
        PartSimState pss = editor.GetSimulation().GetSimState(part);
        IntermediatePartState uco = editor.GetIntermState(part, pos);
        float time = editor.GetCycleTime();
        Vector2 pivot = new(123f, 48f);

        Vector2 offset = new(0, -1f);
        renderer.RenderBase(Textures.Separation.Base, offset, pivot, 0);
        // input
        renderer.RenderShadowCircle(Textures.Separation.ZephironInput, SeparationHoleHex, 0);
        TextureRenderer.Render(Textures.HoleSymbol.Zephiron, (HexGrid.standardGrid.ToPixelCoords(SeparationHoleHex, Vector2.Zero).Rotated(uco.rotation) + uco.pos - (Textures.HoleSymbol.Zephiron.size.ToVector2() / 2)).Rounded());
        int irisFrame = 15;
        bool afterIrisOpens = false;
        Molecule risingAtom = null;
        Vector2 risingOffset = uco.pos + HexGrid.standardGrid.ToPixelCoords(SeparationGelaronIrisHex).Rotated(uco.rotation);

        renderer.RenderRotating(Textures.Separation.GelaronIrisBase, SeparationGelaronIrisHex, Vector2.Zero);
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
        renderer.RenderUpright(Textures.Irises.Gelaron[irisFrame], SeparationGelaronIrisHex, Vector2.Zero);
        renderer.RenderRotating(Textures.Separation.GelaronIrisLip, SeparationGelaronIrisHex, Vector2.Zero);
        if (pss.isProcessing && afterIrisOpens)
        {
            // show atom rising infront of iris
            Editor.RenderMolecule(risingAtom, risingOffset, new HexIndex(0, 0), 0f, 1f, time, 1f, false, null);
        }

        renderer.RenderRotating(Textures.Separation.FrixonIrisBase, SeparationFrixonIrisHex, Vector2.Zero);
        if (pss.isProcessing)
        {
            risingOffset = uco.pos + HexGrid.standardGrid.ToPixelCoords(SeparationFrixonIrisHex).Rotated(uco.rotation);
            risingAtom = Molecule.CreateMonoatomic(pss.processingAtoms[1]);
            if (!afterIrisOpens)
            {
                // show atom rising behind iris
                Editor.RenderMolecule(risingAtom, risingOffset, new HexIndex(0, 0), 0f, 1f, time, 1f, false, null);
            }
        }

        renderer.RenderUpright(Textures.Irises.Frixon[irisFrame], SeparationFrixonIrisHex, Vector2.Zero);
        renderer.RenderRotating(Textures.Separation.FrixonIrisLip, SeparationFrixonIrisHex, Vector2.Zero);
        if (pss.isProcessing && afterIrisOpens)
        {
            // show atom rising infront of iris
            Editor.RenderMolecule(risingAtom, risingOffset, new HexIndex(0, 0), 0f, 1f, time, 1f, false, null);
        }
        renderer.RenderBase(Textures.Separation.Connectors, offset, pivot, 0);
    }
    public static bool OnCycle(Sim sim, Part part, PartSimState pss, GlyphRecipe recipe, bool first)
    {
        if (first)
        {
            HexIndex h = part.InFrontBy(SeparationHoleHex);
            HexIndex iF = part.InFrontBy(SeparationFrixonIrisHex);
            HexIndex iP = part.InFrontBy(SeparationGelaronIrisHex);

            if (sim.FindAtom(iF).HasValue() || sim.FindAtom(iP).HasValue())
            {
                // blocked!
                return false;
            }

            if (!sim.FindAtom(h).GetOrDefault(out AtomReference holeAtom))
            {
                // no atom above hole
                return false;
            }

            if (holeAtom.atomType != Atoms.zephiron || holeAtom.inMultiAtomMolecule || holeAtom.isHeldByArm)
            {
                return false;
            }

            // an unheld, elemental zephiron atom is above the hole, and neither iris is covered
            holeAtom.molecule.RemoveAtom(holeAtom.pos);
            sim.solutionEditor.DrawFallingAtom(holeAtom);

            sim.SpawnColliderRelative(part, SeparationGelaronIrisHex);
            sim.SpawnColliderRelative(part, SeparationFrixonIrisHex);

            pss.isProcessing = true;
            pss.processingAtoms = new AtomType[2] { Atoms.gelaron, Atoms.frixon };

            Sounds.glyph_separation.PlaySound(sim.solutionEditor.method_506());
            return true;
        }
        else if (pss.isProcessing)
        {
            sim.SpawnAtomRelative(pss.processingAtoms[0], part, SeparationGelaronIrisHex);
            sim.SpawnAtomRelative(pss.processingAtoms[1], part, SeparationFrixonIrisHex);
            return false;
        }
        return false;
    }
}
