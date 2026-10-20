namespace NeuvolicsUtil;

public static class Textures
{
    private static readonly string modPath = "/erikhaag/Neuvolics/";
    private static readonly string glyphTexturePath = "textures/parts" + modPath;
    private static readonly string selectTexturePath = "textures/select" + modPath;

    public static class Icon
    {
        public static readonly Texture Separation = AssetLoaderHelper.LoadTexture(glyphTexturePath + "icons/separation");
        public static readonly Texture SeparationHover = AssetLoaderHelper.LoadTexture(glyphTexturePath + "icons/separation_hover");

        public static readonly Texture Fixation = AssetLoaderHelper.LoadTexture(glyphTexturePath + "icons/fixation");
        public static readonly Texture FixationHover = AssetLoaderHelper.LoadTexture(glyphTexturePath + "icons/fixation_hover");

        public static readonly Texture Consolidation = AssetLoaderHelper.LoadTexture(glyphTexturePath + "icons/consolidation");
        public static readonly Texture ConsolidationHover = AssetLoaderHelper.LoadTexture(glyphTexturePath + "icons/consolidation_hover");

        public static readonly Texture Putrefaction = AssetLoaderHelper.LoadTexture(glyphTexturePath + "icons/putrefaction");
        public static readonly Texture PutrefactionHover = AssetLoaderHelper.LoadTexture(glyphTexturePath + "icons/putrefaction_hover");

        public static readonly Texture Cataclysm = AssetLoaderHelper.LoadTexture(glyphTexturePath + "icons/cataclysm");
        public static readonly Texture CataclysmHover = AssetLoaderHelper.LoadTexture(glyphTexturePath + "icons/cataclysm_hover");

        public static readonly Texture Maro = AssetLoaderHelper.LoadTexture(glyphTexturePath + "icons/maro");
        public static readonly Texture MaroHover = AssetLoaderHelper.LoadTexture(glyphTexturePath + "icons/maro_hover");
    }

    public static class Select
    {
        public static readonly Texture ParallelogramGlow = AssetLoaderHelper.LoadTexture(selectTexturePath + "parallelogram_3_glow");
        public static readonly Texture ParallelogramStroke = AssetLoaderHelper.LoadTexture(selectTexturePath + "parallelogram_3_stroke");
        public static readonly Texture TrilineGlow = AssetLoaderHelper.LoadTexture(selectTexturePath + "triline_glow");
        public static readonly Texture TrilineStroke = AssetLoaderHelper.LoadTexture(selectTexturePath + "triline_stroke");
        public static readonly Texture BendGlow = AssetLoaderHelper.LoadTexture(selectTexturePath + "bend_glow");
        public static readonly Texture BendStroke = AssetLoaderHelper.LoadTexture(selectTexturePath + "bend_stroke");
    }

    public static class Putrefaction
    {
        public static readonly Texture Base = AssetLoaderHelper.LoadTexture(glyphTexturePath + "Putrefaction/base");
        public static readonly Texture Solder = AssetLoaderHelper.LoadTexture(glyphTexturePath + "Putrefaction/solder");
        public static readonly Texture RatsNest = AssetLoaderHelper.LoadTexture(glyphTexturePath + "Putrefaction/rats_nest");
    }

    public static class Consolidation
    {
        public static Texture Base = AssetLoaderHelper.LoadTexture(glyphTexturePath + "Consolidation/base");
        public static Texture VolicInput = AssetLoaderHelper.LoadTexture(glyphTexturePath + "Consolidation/volic_input");
        public static Texture IrisBase = AssetLoaderHelper.LoadTexture(glyphTexturePath + "Consolidation/output_under_iris");
        public static Texture IrisLip = AssetLoaderHelper.LoadTexture(glyphTexturePath + "Consolidation/output_above_iris");
        public static Texture Connectors = AssetLoaderHelper.LoadTexture(glyphTexturePath + "Consolidation/connectors");
        public static Texture HoleBar = AssetLoaderHelper.LoadTexture(glyphTexturePath + "Consolidation/separator");
        public static Texture HoleFrixonActive = AssetLoaderHelper.LoadTexture(glyphTexturePath + "Consolidation/frixon_active");
        public static Texture HoleFrixonInactive = AssetLoaderHelper.LoadTexture(glyphTexturePath + "Consolidation/frixon_inactive");
        public static Texture HoleGelaronActive = AssetLoaderHelper.LoadTexture(glyphTexturePath + "Consolidation/gelaron_active");
        public static Texture HoleGelaronInactive = AssetLoaderHelper.LoadTexture(glyphTexturePath + "Consolidation/gelaron_inactive");
    }

    public static class Separation
    {
        public static readonly Texture Base = AssetLoaderHelper.LoadTexture(glyphTexturePath + "Separation/base");
        public static readonly Texture ZephironInput = AssetLoaderHelper.LoadTexture(glyphTexturePath + "Separation/zephiron_input");
        public static readonly Texture FrixonIrisLip = AssetLoaderHelper.LoadTexture(glyphTexturePath + "Separation/frixon_output_above_iris");
        public static readonly Texture FrixonIrisBase = AssetLoaderHelper.LoadTexture(glyphTexturePath + "Separation/frixon_output_under_iris");
        public static readonly Texture GelaronIrisBase = AssetLoaderHelper.LoadTexture(glyphTexturePath + "Separation/gelaron_output_under_iris");
        public static readonly Texture GelaronIrisLip = AssetLoaderHelper.LoadTexture(glyphTexturePath + "Separation/gelaron_output_above_iris");
        public static readonly Texture Connectors = AssetLoaderHelper.LoadTexture(glyphTexturePath + "Separation/connectors");
    }

    public static class Fixation
    {
        public static readonly Texture Base = AssetLoaderHelper.LoadTexture(glyphTexturePath + "Fixation/base");
        public static readonly Texture[] Nets = new Texture[] { AssetLoaderHelper.LoadTexture(glyphTexturePath + "Fixation/base_tr"), AssetLoaderHelper.LoadTexture(glyphTexturePath + "Fixation/base_tl"), AssetLoaderHelper.LoadTexture(glyphTexturePath + "Fixation/base_bl"), AssetLoaderHelper.LoadTexture(glyphTexturePath + "Fixation/base_br") };
        public static readonly Texture Connectors = AssetLoaderHelper.LoadTexture(glyphTexturePath + "Fixation/connectors");
        public static readonly Texture HoleBar = AssetLoaderHelper.LoadTexture(glyphTexturePath + "Fixation/separator");
        public static readonly Texture HoleNeumetalActive = AssetLoaderHelper.LoadTexture(glyphTexturePath + "Fixation/neumetal_active");
        public static readonly Texture HoleNeumetalInactive = AssetLoaderHelper.LoadTexture(glyphTexturePath + "Fixation/neumetal_inactive");
        public static readonly Texture HoleVolicActive = AssetLoaderHelper.LoadTexture(glyphTexturePath + "Fixation/volic_active");
        public static readonly Texture HoleFrixonHalfActive = AssetLoaderHelper.LoadTexture(glyphTexturePath + "Fixation/frixon_half_active");
        public static readonly Texture HoleFrixonInactive = AssetLoaderHelper.LoadTexture(glyphTexturePath + "Fixation/frixon_inactive");
        public static readonly Texture HoleGelaronHalfActive = AssetLoaderHelper.LoadTexture(glyphTexturePath + "Fixation/gelaron_half_active");
        public static readonly Texture HoleGelaronInactive = AssetLoaderHelper.LoadTexture(glyphTexturePath + "Fixation/gelaron_inactive");
    }

    public static class Cataclysm
    {
        public static readonly Texture Base = AssetLoaderHelper.LoadTexture(glyphTexturePath + "Cataclysm/base");
        public static readonly LightDirTextures Pentacle = AssetLoaderHelper.LoadLightDir(glyphTexturePath + "Cataclysm/pentacle.lighting");
        public static readonly Texture Bowl1Overlay = AssetLoaderHelper.LoadTexture(glyphTexturePath + "Cataclysm/bowl1_overlay");
        public static readonly Texture Bowl2Overlay = AssetLoaderHelper.LoadTexture(glyphTexturePath + "Cataclysm/bowl2_overlay");
        public static readonly Texture ZephironInput = AssetLoaderHelper.LoadTexture(glyphTexturePath + "Cataclysm/zephiron_input");
        public static readonly Texture ZephironIrisBase = AssetLoaderHelper.LoadTexture(glyphTexturePath + "Cataclysm/zephiron_output_under_iris");
        public static readonly Texture ZephironIrisLip = AssetLoaderHelper.LoadTexture(glyphTexturePath + "Cataclysm/zephiron_output_above_iris");
    }

    public static class HoleSymbol
    {
        public static readonly Texture Zephiron = AssetLoaderHelper.LoadTexture(glyphTexturePath + "zephiron_hole");
        public static readonly Texture Volic = AssetLoaderHelper.LoadTexture(glyphTexturePath + "volic_hole");
    }

    public static class BowlSymbol
    {
        public static readonly Texture Neumetal = AssetLoaderHelper.LoadTexture(glyphTexturePath + "neumetal_bowl");
        public static readonly Texture Volic = AssetLoaderHelper.LoadTexture(glyphTexturePath + "volic_bowl");
    }

    public static class Irises
    {
        public static readonly Texture[] Frixon = AssetLoaderHelper.LoadAnimation(glyphTexturePath + "iris_full_frixon.array", "iris_full_frixon", 16);
        public static readonly Texture[] Neumetal = AssetLoaderHelper.LoadAnimation(glyphTexturePath + "iris_full_neumetal.array", "iris_full_neumetal", 16);
        public static readonly Texture[] Gelaron = AssetLoaderHelper.LoadAnimation(glyphTexturePath + "iris_full_gelaron.array", "iris_full_gelaron", 16);
        public static readonly Texture[] Zephiron = AssetLoaderHelper.LoadAnimation(glyphTexturePath + "iris_full_zephiron.array", "iris_full_zephiron", 16);
    }
}
