using MonoMod.ModInterop;
using Neuvolics;

namespace Util;

public static class API
{
    private static readonly AtomType[] NeumetalCycle = { Atoms.hestium, Atoms.azulum, Atoms.taceum, Atoms.mitrum, Atoms.iridium };

    public static AtomType GetNeumetalAtom(int i) => NeumetalCycle[Utils.Modulo(i, 5) /* wrap the index in-bounds */];

    public static int GetNeumetalIndex(AtomType aT)
    {
        for (int i = 0; i < 5; i++)
        {
            if (NeumetalCycle[i] == aT)
            {
                return i;
            }
        }
        // if not found
        return -1;
    }

    public static void DrawIris
    (
        PartRenderer renderer,
        IntermediatePartState partDataWrapper,
        HexIndex offset,
        float time,
        Texture[] irisFrames,
        Maybe<AtomType> risingAtom,
        bool forceOpen = false
    )
    {
        int num = irisFrames.Length - 1;
        bool flag = false;
        Molecule param_ = null;
        Vector2 param_2 = partDataWrapper.pos + HexGrid.standardGrid.ToPixelCoords(offset).Rotated(partDataWrapper.rotation);
        bool flag2 = risingAtom.GetOrDefault(out AtomType param_3);
        renderer.RenderRotating(Assets.textures.parts.animismus.output_under_iris, offset, Vector2.Zero);
        if (forceOpen || flag2)
        {
            num = Utils.Clamp((int)(Utils.InterpolateLinear(1f, -1f, time) * (float)irisFrames.Length), 0, irisFrames.Length - 1);
            if (flag2)
            {
                flag = (double)time > 0.5;
                param_ = Molecule.CreateMonoatomic(param_3);
                if (!flag)
                {
                    Editor.RenderMolecule(param_, param_2, new HexIndex(0, 0), 0f, 1f, time, 1f, isOutputRender: false, null);
                }
            }
        }

        renderer.RenderUpright(irisFrames[num], offset, Vector2.Zero);
        renderer.RenderRotating(Assets.textures.parts.animismus.output_above_iris, offset, Vector2.Zero);
        if (flag)
        {
            Editor.RenderMolecule(param_, param_2, new HexIndex(0, 0), 0f, 1f, time, 1f, isOutputRender: false, null);
        }
    }

    public static Maybe<T> ConvertToMaybe<T>(T data)
    {
        if (data != null)
        {
            return Maybe<T>.From(data);
        }

        return MaybeHelper.empty;
    }
}

public class ExportManager
{

    public static void ExportAtoms()
    {
        typeof(AtomExports).ModInterop();
    }

    public static void ExportGlyphs()
    {
        typeof(GlyphExports).ModInterop();
    }

    public static void ExportTextures()
    {
        typeof(FixationTextureExports).ModInterop();
    }

    public static void ExportSounds()
    {
        typeof(SoundExports).ModInterop();
    }

    public static void ExportAll() {
        ExportAtoms();
        ExportGlyphs();
        ExportTextures();
        ExportSounds();
    }

    [ModExportName("Neuvolics.Atoms")]
    public static class AtomExports
    {
        public static AtomType GetAzulum() => Atoms.azulum;
        public static AtomType GetFrixon() => Atoms.frixon;
        public static AtomType GetGelaron() => Atoms.gelaron;
        public static AtomType GetHestium() => Atoms.hestium;
        public static AtomType GetIridium() => Atoms.iridium;
        public static AtomType GetMitrum() => Atoms.mitrum;
        public static AtomType GetTaceum() => Atoms.taceum;
        public static AtomType GetZephiron() => Atoms.zephiron;

        public static AtomType GetNeumetalAtom(int index) => API.GetNeumetalAtom(index);
        public static int GetNeumetalIndex(AtomType neumetal) => API.GetNeumetalIndex(neumetal);
    }

    [ModExportName("Neuvolics.Glyphs")]
    public static class GlyphExports
    {
        public static PartType GetPutrefaction() => Neuvolics.Parts.Putrefaction;
        public static PartType GetConsolidation() => Neuvolics.Parts.Consolidation;
        public static PartType GetSeparation() => Neuvolics.Parts.Separation;
        public static PartType GetFixation() => Neuvolics.Parts.Fixation;
        public static PartType GetCataclysm() => Neuvolics.Parts.Cataclysm;
    }

    [ModExportName("Neuvolics.Textures.Fixation")]
    public static class FixationTextureExports
    {
        public static Texture GetFixationBase() => Textures.Fixation.Base;
        public static Texture[] GetFixationNets() => Textures.Fixation.Nets;
        public static Texture GetFixationConnectors() => Textures.Fixation.Connectors;
        public static Texture GetFixationHoleBar() => Textures.Fixation.HoleBar;
        public static Texture GetFixationHoleNeumetalActive() => Textures.Fixation.HoleNeumetalActive;
        public static Texture GetFixationHoleNeumetalInactive() => Textures.Fixation.HoleNeumetalInactive;
        public static Texture GetFixationHoleVolicActive() => Textures.Fixation.HoleVolicActive;
        public static Texture GetFixationHoleFrixonHalfActive() => Textures.Fixation.HoleFrixonHalfActive;
        public static Texture GetFixationHoleFrixonInactive() => Textures.Fixation.HoleFrixonInactive;
        public static Texture GetFixationHoleGelaronHalfActive() => Textures.Fixation.HoleGelaronHalfActive;
        public static Texture GetFixationHoleGelaronInactive() => Textures.Fixation.HoleGelaronInactive;

        public static Texture[] GetFixationZephironIris() => Textures.Irises.Zephiron;
        public static Texture[] GetFixationNeumetalIris() => Textures.Irises.Neumetal;
    }

    [ModExportName("Neuvolics.Sounds")]
    public static class SoundExports
    {
        public static Sound GetFixationSound() => Sounds.glyph_fixation;
    }

}
