namespace Neuvolics
{
    internal class Sounds
    {
        public static Sound glyph_cataclysm_consume, glyph_cataclysm_discard, glyph_cataclysm_eject, glyph_cataclysm_transfuse_frixon, glyph_cataclysm_transfuse_gelaron, glyph_consolidation, glyph_fixation, glyph_putrefaction, glyph_separation;
        public static void AddSounds()
        {
            glyph_cataclysm_consume = AssetLoaderHelper.LoadSound("sounds/cataclysmConsume");
            glyph_cataclysm_discard = AssetLoaderHelper.LoadSound("sounds/cataclysmDiscard");
            glyph_cataclysm_eject = AssetLoaderHelper.LoadSound("sounds/cataclysmEject");
            glyph_cataclysm_transfuse_frixon = AssetLoaderHelper.LoadSound("sounds/cataclysmTransfuseFrix");
            glyph_cataclysm_transfuse_gelaron = AssetLoaderHelper.LoadSound("sounds/cataclysmTransfuseGel");
            glyph_consolidation = AssetLoaderHelper.LoadSound("sounds/consolidation");
            glyph_fixation = AssetLoaderHelper.LoadSound("sounds/fixation");
            glyph_putrefaction = AssetLoaderHelper.LoadSound("sounds/putrefaction");
            glyph_separation = AssetLoaderHelper.LoadSound("sounds/separation");

            Volume.SetVoulme(glyph_cataclysm_consume, 0.35f);
            Volume.SetVoulme(glyph_cataclysm_discard, 0.35f);
            Volume.SetVoulme(glyph_cataclysm_eject, 0.35f);
            Volume.SetVoulme(glyph_cataclysm_transfuse_frixon, 0.35f);
            Volume.SetVoulme(glyph_cataclysm_transfuse_gelaron, 0.35f);
            Volume.SetVoulme(glyph_consolidation, 0.35f);
            Volume.SetVoulme(glyph_fixation, 0.35f);
            Volume.SetVoulme(glyph_putrefaction, 0.35f);
            Volume.SetVoulme(glyph_separation, 0.35f);
        }
    }
}
