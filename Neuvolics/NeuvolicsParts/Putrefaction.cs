using Neuvolics;
using Quintessential;
using NeuvolicsUtil;

namespace NeuvolicsParts;

internal static class PutrefactionGlyph {

    public static readonly HexIndex PutrefactionBowlHex = new(1, 0);
    public static readonly HexIndex PutrefactionHole1Hex = new(0, -1);
    public static readonly HexIndex PutrefactionHole2Hex = new(-1, 1);
    public static readonly HexIndex PutrefactionWiresHex = new(0, 0);

    public static void Render(Part part, Vector2 pos, SolutionEditorBase editor, PartRenderer renderer)
    {
        Vector2 pivot = new(83f, 119f);
        Vector2 offset = new(0, -1f);
        renderer.RenderBase(Textures.Putrefaction.Base, offset, pivot, 0);
        renderer.RenderBase(Textures.Putrefaction.Solder, offset, pivot, 0);
        renderer.RenderBase(Textures.Putrefaction.RatsNest, offset, pivot, 0);

        HexIndex[] holes = new HexIndex[]
        {
            PutrefactionHole1Hex, PutrefactionHole2Hex
        };

        foreach (HexIndex h in holes)
        {
            renderer.RenderShadowCircle(Assets.textures.parts.projection_glyph.quicksilver_input, h, 0);
            renderer.RenderUpright(Textures.HoleSymbol.Volic, h, Vector2.Zero);
        }

        renderer.RenderRotating(Assets.textures.parts.calcinator_bowl, PutrefactionBowlHex, Vector2.Zero);
        renderer.RenderUpright(Textures.BowlSymbol.Neumetal, PutrefactionBowlHex, Vector2.Zero);

    }
    public static bool OnCycle(Sim sim, Part part, PartSimState pss, GlyphRecipe recipe, bool first)
    {
        HexIndex bowl = part.InFrontBy(PutrefactionBowlHex);
        HexIndex hole1 = part.InFrontBy(PutrefactionHole1Hex);
        HexIndex hole2 = part.InFrontBy(PutrefactionHole2Hex);
        if (!sim.FindAtom(bowl).GetOrDefault(out AtomReference bowlAtom))
        {
            //bowl empty
            return false;
        }

        int neumetalIndex = API.GetNeumetalIndex(bowlAtom.atomType);
        if (neumetalIndex == -1)
        {
            // invalid atom
            return false;
        }

        bool consumeHole1 = sim.FindAtom(hole1).GetOrDefault(out AtomReference hole1Atom) && !hole1Atom.inMultiAtomMolecule && !hole1Atom.isHeldByArm;
        bool consumeHole2 = sim.FindAtom(hole2).GetOrDefault(out AtomReference hole2Atom) && !hole2Atom.inMultiAtomMolecule && !hole2Atom.isHeldByArm;

        if (consumeHole1)
        {
            if (hole1Atom.atomType == Atoms.frixon)
            {
                neumetalIndex += 1;
            }
            else if (hole1Atom.atomType == Atoms.gelaron)
            {
                neumetalIndex -= 1;
            }
            else
            {
                consumeHole1 = false;
            }
        }
        if (consumeHole2)
        {
            if (hole2Atom.atomType == Atoms.frixon)
            {
                neumetalIndex += 1;
            }
            else if (hole2Atom.atomType == Atoms.gelaron)
            {
                neumetalIndex -= 1;
            }
            else
            {
                consumeHole2 = false;
            }
        }

        if (!consumeHole1 && !consumeHole2)
        {
            // neither atom could be consumed
            return false;
        }

        if (consumeHole1)
        {
            hole1Atom.molecule.RemoveAtom(hole1Atom.pos);
            sim.solutionEditor.DrawFallingAtom(hole1Atom);
        }
        if (consumeHole2)
        {
            hole2Atom.molecule.RemoveAtom(hole2Atom.pos);
            sim.solutionEditor.DrawFallingAtom(hole2Atom);
        }

        bowlAtom.molecule.ReplaceAtom(API.GetNeumetalAtom(neumetalIndex), bowlAtom.pos);
        Sounds.glyph_putrefaction.PlaySound(sim.solutionEditor.method_506());
        bowlAtom.atom.transmutationEffect = new TransmutationEffect(sim.solutionEditor, (TransmutationEffectRenderMode) 1, bowlAtom.atomType, Assets.textures.atoms.projection_effect, 30f);

        return true;
    }
}
