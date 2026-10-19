using Quintessential;
using System.Collections.Generic;

namespace Neuvolics;

internal static class Wheels
{

    public static PartType MarosWheel;

    public static void AddWheelTypes() {
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

}
