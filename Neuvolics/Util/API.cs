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
}
