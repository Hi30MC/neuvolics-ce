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
