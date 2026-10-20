using Quintessential;
using System.Collections.Generic;
using System.Linq;

namespace Neuvolics;

internal static class Wheels
{

    public static PartType MarosWheel;
    static Molecule MaroMolecule()
    {
        Molecule molecule = new();
        molecule.AddAtom(new(Atoms.gelaron), new HexIndex(0, 1));
        molecule.AddAtom(new(Atoms.zephiron), new HexIndex(1, 0));
        molecule.AddAtom(new(Atoms.frixon), new HexIndex(1, -1));
        molecule.AddAtom(new(Atoms.gelaron), new HexIndex(0, -1));
        molecule.AddAtom(new(Atoms.zephiron), new HexIndex(-1, 0));
        molecule.AddAtom(new(Atoms.frixon), new HexIndex(-1, 1));
        return molecule;
    }


    public static void AddWheelTypes()
    {
        MarosWheel = new()
        {
            baseTexture = AssetLoaderHelper.LoadTexture("textures/parts/erikhaag/Neuvolics/icons/maro"), // Panel icon
            hoverTexture = AssetLoaderHelper.LoadTexture("textures/parts/erikhaag/Neuvolics/icons/maro_hover"), // Hovered panel icon
            cost = 25,
            isUnique = true,
            layer = PartTypeLayer.Top,
            isProgrammable = true,
            attachedAtoms = new Dictionary<HexIndex, AtomType>
            {
                {new HexIndex(0, 1), Atoms.gelaron},
                {new HexIndex(1, 0), Atoms.zephiron},
                {new HexIndex(1, -1), Atoms.frixon},
                {new HexIndex(0, -1), Atoms.gelaron},
                {new HexIndex(-1, 0), Atoms.zephiron},
                {new HexIndex(-1, 1), Atoms.frixon}
            },
            canRotateInSimulation = true,
            permissionCategory = PuzzlePermissions.None,
            CustomPermissionCheck = perms => perms.Contains(Neuvolics.Instance.GetIdentifier("maros_wheel"))
        };

        QApi.AddPartTypeToPanel(MarosWheel, false);
        Neuvolics.Instance.AddPartType(MarosWheel, "maros_wheel", (part, pos, editor, renderer) => { });
        Neuvolics.Instance.AddPuzzlePermission("maros_wheel");
    }

    public static Maybe<AtomReference> MaybeFindMaroWheelAtom(Sim sim_self, Part part, HexIndex offset) => MaybeFindMaroWheelAtom(sim_self, part.InFrontBy(offset));
    public static Maybe<AtomReference> MaybeFindMaroWheelAtom(Sim sim_self, HexIndex hex)
    {
        var SEB = sim_self.solutionEditor;
        var solution = SEB.GetSolution();
        var partList = solution.parts;
        var partSimStates = sim_self.simulationDict;

        foreach (var maro in partList.Where(x => x.GetType() == MarosWheel))
        {
            var partSimState = partSimStates[maro];
            Molecule maroAtoms = MaroMolecule();
            var hexIndex = partSimState.newPosition;
            var rotation = partSimState.rotation;
            var hexKey = (hex - hexIndex).Rotated(rotation.Negative());

            if (maroAtoms.GetAtoms().TryGetValue(hexKey, out Atom atom))
            {
                return new AtomReference(maroAtoms, hexKey, atom.atomType, atom, true);
            }
        }
        return MaybeHelper.empty;
    }

}
