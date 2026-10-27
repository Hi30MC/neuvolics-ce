using NeuvolicsParts;
using Quintessential;
using NeuvolicsUtil;
using static Quintessential.PartCycleDelegate;
namespace Neuvolics;

internal static class Parts
{
    public static PartType Cataclysm, Consolidation, Fixation, Putrefaction, Separation;

    public static void AddPartTypes()
    {
        Cataclysm = new()
        {
            cost = 20,
            isFullHexCover = true,
            glowTexture = Assets.textures.select.disposal_glow, // TBD
            strokeTexture = Assets.textures.select.disposal_stroke, // TBD
            baseTexture = Textures.Icon.Cataclysm,
            hoverTexture = Textures.Icon.CataclysmHover,
            glyphHexes = new HexIndex[] {
                CataclysmGlyph.CataclysmHoleHex,
                CataclysmGlyph.CataclysmIrisHex,
                CataclysmGlyph.CataclysmBowl1Hex,
                CataclysmGlyph.CataclysmBowl2Hex
            },
            permissionCategory = PuzzlePermissions.None,
            CustomPermissionCheck = perms => perms.Contains(Neuvolics.Instance.GetIdentifier("cataclysm"))
        };
        Consolidation = new()
        {
            cost = 10,
            isFullHexCover = true,
            glowTexture = Textures.Select.BendGlow,
            strokeTexture = Textures.Select.BendStroke,
            baseTexture = Textures.Icon.Consolidation,
            hoverTexture = Textures.Icon.ConsolidationHover,
            glyphHexes = new HexIndex[] {
                ConsolidationGlyph.ConsolidationHole1Hex,
                ConsolidationGlyph.ConsolidationHole2Hex,
                ConsolidationGlyph.ConsolidationZephironIrisHex,
            },
            permissionCategory = PuzzlePermissions.None,
            CustomPermissionCheck = perms => perms.Contains(Neuvolics.Instance.GetIdentifier("consolidation"))
        };
        Fixation = new()
        {
            cost = 15,
            isFullHexCover = true,
            glowTexture = Textures.Select.ParallelogramGlow,
            strokeTexture = Textures.Select.ParallelogramStroke,
            baseTexture = Textures.Icon.Fixation,
            hoverTexture = Textures.Icon.FixationHover,
            glyphHexes = new HexIndex[] {
                FixationGlyph.FixationHole1Hex,
                FixationGlyph.FixationHole2Hex,
                FixationGlyph.FixationHole3Hex,
                FixationGlyph.FixationZephironIrisHex,
                FixationGlyph.FixationNeumetalIrisHex,
            },
            permissionCategory = PuzzlePermissions.None,
            CustomPermissionCheck = perms => perms.Contains(Neuvolics.Instance.GetIdentifier("fixation"))
        };
        Putrefaction = new()
        {
            cost = 15,
            isFullHexCover = true,
            glowTexture = Assets.textures.select.tetra_glow,
            strokeTexture = Assets.textures.select.tetra_stroke,
            baseTexture = Textures.Icon.Putrefaction,
            hoverTexture = Textures.Icon.PutrefactionHover,
            glyphHexes = new HexIndex[] {
                PutrefactionGlyph.PutrefactionHole1Hex,
                PutrefactionGlyph.PutrefactionHole2Hex,
                PutrefactionGlyph.PutrefactionBowlHex,
                PutrefactionGlyph.PutrefactionWiresHex,
            },
            permissionCategory = PuzzlePermissions.None,
            CustomPermissionCheck = perms => perms.Contains(Neuvolics.Instance.GetIdentifier("putrefaction"))
        };
        Separation = new()
        {
            cost = 15,
            isFullHexCover = true,
            glowTexture = Textures.Select.TrilineGlow,
            strokeTexture = Textures.Select.TrilineStroke,
            baseTexture = Textures.Icon.Separation,
            hoverTexture = Textures.Icon.SeparationHover,
            glyphHexes = new HexIndex[] {
                SeparationGlyph.SeparationFrixonIrisHex,
                SeparationGlyph.SeparationGelaronIrisHex,
                SeparationGlyph.SeparationHoleHex,
            },
            permissionCategory = PuzzlePermissions.None,
            CustomPermissionCheck = perms => perms.Contains(Neuvolics.Instance.GetIdentifier("separation"))
        };

        Neuvolics.Instance.AddPartType(Cataclysm, "cataclysm", CataclysmGlyph.Render);
        Neuvolics.Instance.AddPartType(Consolidation, "consolidation", ConsolidationGlyph.Render);
        Neuvolics.Instance.AddPartType(Fixation, "fixation", FixationGlyph.Render);
        Neuvolics.Instance.AddPartType(Putrefaction, "putrefaction", PutrefactionGlyph.Render);
        Neuvolics.Instance.AddPartType(Separation, "separation", SeparationGlyph.Render);

        QApi.AddPartTypeToPanel(Separation, PartTypes.equilibriumGlyph);
        QApi.AddPartTypeToPanel(Consolidation, Separation);
        QApi.AddPartTypeToPanel(Fixation, Consolidation);
        QApi.AddPartTypeToPanel(Putrefaction, Fixation);
        QApi.AddPartTypeToPanel(Cataclysm, Putrefaction);

        Neuvolics.Instance.AddPuzzlePermission("cataclysm");
        Neuvolics.Instance.AddPuzzlePermission("consolidation");
        Neuvolics.Instance.AddPuzzlePermission("fixation");
        Neuvolics.Instance.AddPuzzlePermission("putrefaction");
        Neuvolics.Instance.AddPuzzlePermission("separation");

        QApi.BindGlyphCycle(Cataclysm, new(PartCycleExecutionType.Normal, CataclysmGlyph.OnCycle));
        QApi.BindGlyphCycle(Consolidation, new(PartCycleExecutionType.Normal, ConsolidationGlyph.OnCycle));
        QApi.BindGlyphCycle(Fixation, new(PartCycleExecutionType.Normal, FixationGlyph.OnCycle));
        QApi.BindGlyphCycle(Putrefaction, new(PartCycleExecutionType.Normal, PutrefactionGlyph.OnCycle));
        QApi.BindGlyphCycle(Separation, new(PartCycleExecutionType.Normal, SeparationGlyph.OnCycle));

        Neuvolics.Instance.AddRecipe(new(), Cataclysm.Id, Cataclysm.Id);
        Neuvolics.Instance.AddRecipe(new(), Consolidation.Id, Consolidation.Id);
        Neuvolics.Instance.AddRecipe(new(), Fixation.Id, Fixation.Id);
        Neuvolics.Instance.AddRecipe(new(), Putrefaction.Id, Putrefaction.Id);
        Neuvolics.Instance.AddRecipe(new(), Separation.Id, Separation.Id);
    }

    public static void EnableTriplex()
    {
        PartTypes.triplexBonder.bonders[0].uniqueAtoms.Add(Atoms.frixon);
        PartTypes.triplexBonder.bonders[0].uniqueAtoms.Add(Atoms.gelaron);
        PartTypes.triplexBonder.bonders[0].uniqueAtoms.Add(Atoms.azulum);
        PartTypes.triplexBonder.bonders[0].uniqueAtoms.Add(Atoms.iridium);
        PartTypes.triplexBonder.bonders[1].uniqueAtoms.Add(Atoms.frixon);
        PartTypes.triplexBonder.bonders[1].uniqueAtoms.Add(Atoms.gelaron);
        PartTypes.triplexBonder.bonders[1].uniqueAtoms.Add(Atoms.azulum);
        PartTypes.triplexBonder.bonders[1].uniqueAtoms.Add(Atoms.iridium);
        PartTypes.triplexBonder.bonders[2].uniqueAtoms.Add(Atoms.frixon);
        PartTypes.triplexBonder.bonders[2].uniqueAtoms.Add(Atoms.gelaron);
        PartTypes.triplexBonder.bonders[2].uniqueAtoms.Add(Atoms.azulum);
        PartTypes.triplexBonder.bonders[2].uniqueAtoms.Add(Atoms.iridium);
    }
}
