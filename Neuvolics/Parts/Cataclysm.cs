using System;
using MonoMod.Utils;
using Neuvolics;
using Quintessential;
using Util;

namespace Parts;

internal static class CataclysmGlyph {

    public static readonly HexIndex CataclysmHoleHex = new(-1, 1);
    public static readonly HexIndex CataclysmIrisHex = new(0, 0);
    public static readonly HexIndex CataclysmBowl1Hex = new(1, 0);
    public static readonly HexIndex CataclysmBowl2Hex = new(0, -1);

    public static void Render(Part part, Vector2 pos, SolutionEditorBase editor, PartRenderer renderer)
    {
        PartSimState pss = editor.GetSimulation().GetSimState(part);
        IntermediatePartState uco = editor.GetIntermState(part, pos);
        float time = editor.GetCycleTime();

        Vector2 pivot = new(123, 120);
        Vector2 offset = Vector2.Zero;

        renderer.RenderBase(Textures.Cataclysm.Base, offset, pivot, 0);
        renderer.RenderTrackLights(Textures.Cataclysm.Pentacle, CataclysmIrisHex, offset, pivot, 0);


        renderer.RenderShadowCircle(Textures.Cataclysm.ZephironInput, CataclysmHoleHex, 0);
        renderer.RenderUpright(Textures.HoleSymbol.Zephiron, CataclysmHoleHex, Vector2.Zero);

        renderer.RenderRotating(Assets.textures.parts.projection_glyph.metal_bowl, CataclysmBowl1Hex, Vector2.Zero);
        renderer.RenderUpright(Textures.BowlSymbol.Volic, CataclysmBowl1Hex, Vector2.Zero);
        renderer.RenderShadowCircle(Textures.Cataclysm.Bowl1Overlay, CataclysmBowl1Hex, 0);

        renderer.RenderRotating(Assets.textures.parts.projection_glyph.metal_bowl, CataclysmBowl2Hex, Vector2.Zero);
        renderer.RenderUpright(Textures.BowlSymbol.Volic, CataclysmBowl2Hex, Vector2.Zero);
        renderer.RenderShadowCircle(Textures.Cataclysm.Bowl2Overlay, CataclysmBowl2Hex, 0);

        // irises
        int irisFrame = 15;
        bool afterIrisOpens = false;
        Molecule risingAtom = null;
        Vector2 risingOffset = uco.pos + HexGrid.standardGrid.ToPixelCoords(CataclysmIrisHex).Rotated(uco.rotation);

        renderer.RenderRotating(Textures.Cataclysm.ZephironIrisBase, CataclysmIrisHex, Vector2.Zero);
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
        renderer.RenderUpright(Textures.Irises.Zephiron[irisFrame], CataclysmIrisHex, Vector2.Zero);
        renderer.RenderRotating(Textures.Cataclysm.ZephironIrisLip, CataclysmIrisHex, Vector2.Zero);
        if (pss.isProcessing && afterIrisOpens)
        {
            // show atom rising infront of iris
            Editor.RenderMolecule(risingAtom, risingOffset, new HexIndex(0, 0), 0f, 1f, time, 1f, false, null);
        }
    }
    public static bool OnCycle(Sim sim, Part part, PartSimState pss, GlyphRecipe recipe, bool first)
    {
        if (first)
        {
            DynamicData pss_data = new(pss);
            object stateObj = pss_data.Get("state");
            CataclysmState cataclysmState = stateObj is not null ? (CataclysmState)stateObj : new();
            if (!(sim.FindAtomRelative(part, CataclysmHoleHex).GetOrDefault(out AtomReference zephiron)
                && zephiron.atomType == Atoms.zephiron && !zephiron.inMultiAtomMolecule && !zephiron.isHeldByArm))
            {
                goto tryTransmute;
            }
            zephiron.molecule.RemoveAtom(zephiron.pos);
            sim.solutionEditor.DrawFallingAtom(zephiron);
            if (cataclysmState.ZephironCount < 6)
            {
                cataclysmState.ZephironCount++;
                Sounds.glyph_cataclysm_consume.PlaySound(sim.solutionEditor.method_506());
            }
            else
            {
                // todo: flash
                Sounds.glyph_cataclysm_discard.PlaySound(sim.solutionEditor.method_506());
            }
        tryTransmute:
            bool bowl1Maro = false;
            bool bowl2Maro = false;
            if (!sim.FindAtomRelative(part, CataclysmBowl1Hex).GetOrDefault(out AtomReference bowl1Atom))
            {
                if (!Wheels.MaybeFindMaroWheelAtom(sim, part, CataclysmBowl1Hex).GetOrDefault(out bowl1Atom))
                {
                    goto tryEject;
                }
                bowl1Maro = true;
            }
            if (!sim.FindAtomRelative(part, CataclysmBowl2Hex).GetOrDefault(out AtomReference bowl2Atom))
            {
                if (bowl1Maro || !Wheels.MaybeFindMaroWheelAtom(sim, part, CataclysmBowl2Hex).GetOrDefault(out bowl2Atom))
                {
                    // if bowl1 also has Maro's wheel, nothing will happen
                    goto tryEject;
                }
                bowl2Maro = true;
            }
            else
            {
            }

            int state = bowl1Maro ? 1 : (bowl2Maro ? 2 : 0);
            AtomReference bowlVolic = null;
            AtomReference bowlZephiron = null;
            if (bowl1Atom.atomType == Atoms.zephiron)
            {
                state = 3 - state;
                bowlVolic = bowl2Atom;
                bowlZephiron = bowl1Atom;
            }
            else if (bowl2Atom.atomType == Atoms.zephiron)
            {

                bowlVolic = bowl1Atom;
                bowlZephiron = bowl2Atom;
            }
            else
            {
                // no zephiron
                goto tryEject;
            }
            bool transfusingFrixon = bowlVolic.atomType == Atoms.frixon;
            if (!transfusingFrixon && bowlVolic.atomType != Atoms.gelaron)
            {
                // no volic.
                goto tryEject;
            }
            // transfuse
            if (state != 1)
            {
                bowlVolic.molecule.ReplaceAtom(Atoms.zephiron, bowlVolic.pos);
            }

            if (state != 2)
            {
                bowlZephiron.molecule.ReplaceAtom(transfusingFrixon ? Atoms.gelaron : Atoms.frixon, bowlZephiron.pos);
            }

            bowlVolic.atom.transmutationEffect = new TransmutationEffect(sim.solutionEditor, (TransmutationEffectRenderMode) 1, bowlVolic.atomType, Assets.textures.atoms.projection_effect, 30f);
            bowlZephiron.atom.transmutationEffect = new TransmutationEffect(sim.solutionEditor, (TransmutationEffectRenderMode) 1, bowlZephiron.atomType, Assets.textures.atoms.projection_effect, 30f);
            if (transfusingFrixon) {
                Sounds.glyph_cataclysm_transfuse_frixon.PlaySound(sim.solutionEditor.method_506());
            } else {
                Sounds.glyph_cataclysm_transfuse_gelaron.PlaySound(sim.solutionEditor.method_506());
            }
        tryEject:
            if (cataclysmState.ZephironCount == 0 || sim.FindAtomRelative(part, CataclysmIrisHex).HasValue())
            {
                // blocked!
                goto setState;
            }
            cataclysmState.ZephironCount--;
            sim.SpawnColliderRelative(part, CataclysmIrisHex);
            Sounds.glyph_cataclysm_eject.PlaySound(sim.solutionEditor.method_506());
            pss.isProcessing = true;
            pss.processingAtoms = new AtomType[] { Atoms.zephiron };
        setState:
            pss_data.Set("state", cataclysmState);
        }
        else if (pss.isProcessing)
        {
            sim.SpawnAtomRelative(pss.processingAtoms[0], part, CataclysmIrisHex);
        }
        return false;
    }
}
