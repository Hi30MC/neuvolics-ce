using MonoMod;
using Neuvolics;
using Quintessential;
using System.Linq;
using System.Runtime.Intrinsics.X86;

public class patch_SoundAssets : SoundAssets
{
    public patch_SoundAssets() : base() {}

    private extern void orig_ResetPlayingSounds();
    private void ResetPlayingSounds()
    {
        orig_ResetPlayingSounds();
        Sounds.glyph_cataclysm_consume.isPlaying = false;
        Sounds.glyph_cataclysm_discard.isPlaying = false;
        Sounds.glyph_cataclysm_eject.isPlaying = false;
        Sounds.glyph_cataclysm_transfuse_frixon.isPlaying = false;
        Sounds.glyph_cataclysm_transfuse_gelaron.isPlaying = false;
        Sounds.glyph_consolidation.isPlaying = false;
        Sounds.glyph_putrefaction.isPlaying = false;
        Sounds.glyph_putrefaction.isPlaying = false;
        Sounds.glyph_separation.isPlaying = false;
    }
}
