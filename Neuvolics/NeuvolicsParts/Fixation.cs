using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Neuvolics;
using Quintessential;
using NeuvolicsUtil;

namespace NeuvolicsParts;

internal static class FixationGlyph {

    public static readonly HexIndex FixationHole1Hex = new(-1, 0);
    public static readonly HexIndex FixationHole2Hex = new(0, 0);
    public static readonly HexIndex FixationHole3Hex = new(1, 0);
    public static readonly HexIndex FixationZephironIrisHex = new(1, -2);
    public static readonly HexIndex FixationNeumetalIrisHex = new(-1, 2);

    internal static HashSet<HexIndex> OccupiedHexes = [];
    public static bool OccupiedHexesStale = true;

    public static void Render(Part part, Vector2 pos, SolutionEditorBase editor, PartRenderer renderer)
    {
        // hold on to your butts!
        PartSimState pss = editor.GetSimulation().GetSimState(part);
        IntermediatePartState partDataWrapper = editor.GetIntermState(part, pos);
        float time = editor.GetCycleTime();

        if (OccupiedHexesStale)
        {
            OccupiedHexes.Clear();
            OccupiedHexes.UnionWith(editor.GetSolution().GetOccupiedHexes(MaybeHelper.empty, (PartLayer)0));
            OccupiedHexesStale = false;
        }

        int invertedNetMask = 0;

        HexIndex[] nettingHexes = new HexIndex[4] { new(0, 1), new(-1, 1), new(0, -1), new(1, -1) };


        IScreen TOS = GameLogic.instance.GetCurrentScreen();

        if (TOS is not SolutionEditorScreen)
        {
            // Not interacting with it, not dragging
            goto NetRemoval;
        }

        IInputMode mode = ((SolutionEditorScreen)TOS).currentInputMode;

        if (mode is not PartDraggingInputMode)
        {
            // not dragging at all
            goto NetRemoval;
        }


        PartDraggingInputMode drag = (PartDraggingInputMode)mode;

        if (((List<PartDraggingInputMode.DraggedPart>)typeof(PartDraggingInputMode).GetField("draggedParts", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(drag)).Any((d) => d.part == part))
        {
            // we are dragging, and this part is in it.
            goto FixationDrawing;
        }

    NetRemoval:
        for (int i = 0; i < nettingHexes.Length; i++)
        {
            if (OccupiedHexes.Contains(part.InFrontBy(nettingHexes[i])))
            {
                // if hex contains a part, disable it
                invertedNetMask |= 1 << i;
            }
        }


    FixationDrawing:
        int atomsPresent = 0;
        HexIndex[] holes = new HexIndex[] { FixationHole1Hex, FixationHole2Hex, FixationHole3Hex };
        foreach (HexIndex h in holes)
        {
            foreach (Molecule m in editor.GetSimulation().GetMolecules())
            {
                if (m.GetAtoms().Count == 1 && m.GetAtoms().TryGetValue(part.InFrontBy(h), out Atom a))
                {
                    AtomType aT = a.atomType;
                    if (aT == Atoms.frixon)
                    {
                        atomsPresent |= (atomsPresent & 1) != 0 ? 4 : 1;
                    }
                    else if (aT == Atoms.gelaron)
                    {
                        atomsPresent |= (atomsPresent & 2) != 0 ? 4 : 2;
                    }
                    else if (API.GetNeumetalIndex(aT) != -1)
                    {
                        atomsPresent |= 8;
                    }
                }
            }
        }

        Vector2 pivot = new(122, 191);
        Vector2 offset = Vector2.Zero;
        renderer.RenderBase(Textures.Fixation.Base, offset, pivot, 0);

        for (int i = 0; i < nettingHexes.Length; i++)
        {
            if (((invertedNetMask >> i) & 1) == 0)
            {
                renderer.RenderBase(Textures.Fixation.Nets[i], offset, pivot, 0);
            }
        }

        // input rendering
        {
            Texture neumetalReadout = (atomsPresent & 8) != 0 ? Textures.Fixation.HoleNeumetalInactive : Textures.Fixation.HoleNeumetalActive;
            Texture volicReadout = Textures.Fixation.HoleVolicActive;
            switch (atomsPresent & 7)
            {
                case 1:
                    // frixon x1
                    volicReadout = Textures.Fixation.HoleFrixonHalfActive;
                    break;
                case 2:
                    // gelaron x1
                    volicReadout = Textures.Fixation.HoleGelaronHalfActive;
                    break;
                case 5:
                    // frixon x2
                    volicReadout = Textures.Fixation.HoleFrixonInactive;
                    break;
                case 6:
                    // gelaron x2
                    volicReadout = Textures.Fixation.HoleGelaronInactive;
                    break;
                default:
                    // 0: no volics are present.
                    // 3: 1 frixon and 1 gelaron are present, invalid.
                    // 4: impossible state.
                    // 7: 2 frixon and 1 gelaron -or- 1 frixon and 2 gelaron are present, also invalid.
                    break;
            }
            foreach (HexIndex h in holes)
            {
                renderer.RenderShadowCircle(Assets.textures.parts.projection_glyph.quicksilver_input, h, 0);
                renderer.RenderUpright(Textures.Fixation.HoleBar, h, Vector2.Zero);
                renderer.RenderUpright(neumetalReadout, h, Vector2.Zero);
                renderer.RenderUpright(volicReadout, h, Vector2.Zero);
            }
        }

        // irises
        API.DrawIris(renderer, partDataWrapper, FixationZephironIrisHex, time, Textures.Irises.Zephiron, pss.isProcessing ? API.ConvertToMaybe(pss.processingAtoms[0]) : MaybeHelper.empty);
        API.DrawIris(renderer, partDataWrapper, FixationNeumetalIrisHex, time, Textures.Irises.Neumetal, pss.isProcessing ? API.ConvertToMaybe(pss.processingAtoms[1]) : MaybeHelper.empty);


        renderer.RenderBase(Textures.Fixation.Connectors, offset, pivot, 0);
    }

    public static bool OnCycle(Sim sim, Part part, PartSimState pss, GlyphRecipe recipe, bool first)
    {
        if (first)
        {
            HexIndex hLeft = part.InFrontBy(FixationHole1Hex);
            HexIndex hCenter = part.InFrontBy(FixationHole2Hex);
            HexIndex hRight = part.InFrontBy(FixationHole3Hex);
            HexIndex iZ = part.InFrontBy(FixationZephironIrisHex);
            HexIndex iN = part.InFrontBy(FixationNeumetalIrisHex);

            if (sim.FindAtom(iZ).HasValue() || sim.FindAtom(iN).HasValue())
            {
                // blocked!
                return false;
            }

            AtomReference neumetal = null;
            AtomReference volic1 = null;
            AtomReference volic2 = null;

            int neumetalIndex = -1;

            HexIndex[] holes = new HexIndex[] { hLeft, hCenter, hRight };
            foreach (HexIndex h in holes)
            {
                if (!sim.FindAtom(h).GetOrDefault(out AtomReference r) || r.inMultiAtomMolecule || r.isHeldByArm)
                {
                    // atom not present, not a singleton, or is held
                    return false;
                }
                if (r.atomType == Atoms.gelaron || r.atomType == Atoms.frixon)
                {
                    if (volic1 == null)
                    {
                        volic1 = r;
                        continue;
                    }
                    if (r.atomType != volic1.atomType)
                    {
                        // second FN is different from the first
                        return false;
                    }
                    if (volic2 == null)
                    {
                        volic2 = r;
                        continue;
                    }

                    return false;
                }
                if (neumetalIndex != -1)
                {
                    // second neumetal, or invalid atom
                    return false;
                }
                neumetalIndex = API.GetNeumetalIndex(r.atomType);
                if (neumetalIndex == -1)
                {
                    // invalid atom present
                    return false;
                }
                neumetal = r;
            }

            int delta = volic1.atomType == Atoms.gelaron ? -1 : 1;

            neumetal.molecule.RemoveAtom(neumetal.pos);
            volic1.molecule.RemoveAtom(volic1.pos);
            volic2.molecule.RemoveAtom(volic2.pos);

            sim.solutionEditor.DrawFallingAtom(neumetal);
            sim.solutionEditor.DrawFallingAtom(volic1);
            sim.solutionEditor.DrawFallingAtom(volic2);

            sim.SpawnColliderRelative(part, FixationZephironIrisHex);
            sim.SpawnColliderRelative(part, FixationNeumetalIrisHex);

            pss.isProcessing = true;
            pss.processingAtoms = new AtomType[2] { Atoms.zephiron, API.GetNeumetalAtom(neumetalIndex + delta) };

            Sounds.glyph_fixation.PlaySound(sim.solutionEditor.method_506());
            return true;
        }
        else if (pss.isProcessing)
        {
            if (pss.processingAtoms[0] is not null)
            {
                sim.SpawnAtomRelative(pss.processingAtoms[0], part, FixationZephironIrisHex);
            }
            if (pss.processingAtoms[1] is not null)
            {
                sim.SpawnAtomRelative(pss.processingAtoms[1], part, FixationNeumetalIrisHex);
            }
            return true;
        }
        return false;
    }
}
