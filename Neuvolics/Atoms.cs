using Quintessential;

namespace Neuvolics;

public static class Atoms
{
    public static AtomType frixon, gelaron, zephiron, hestium, azulum, taceum, mitrum, iridium;

    public static void LoadAtoms()
    {
        Neuvolics.Instance.AddAtomType(frixon, "frixon");
        Neuvolics.Instance.AddAtomType(gelaron, "gelaron");
        Neuvolics.Instance.AddAtomType(zephiron, "zephiron");
        Neuvolics.Instance.AddAtomType(hestium, "hestium");
        Neuvolics.Instance.AddAtomType(azulum, "azulum");
        Neuvolics.Instance.AddAtomType(taceum, "taceum");
        Neuvolics.Instance.AddAtomType(mitrum, "mitrum");
        Neuvolics.Instance.AddAtomType(iridium, "iridium");
    }

}
