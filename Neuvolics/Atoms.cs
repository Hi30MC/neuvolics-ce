using Quintessential;

namespace Neuvolics;

public static class Atoms
{
    public static AtomType frixon, gelaron, zephiron, hestium, azulum, taceum, mitrum, iridium;

    public static void LoadAtoms()
    {
        frixon = new()
        {
            symbol = AssetLoaderHelper.LoadTexture("textures/atoms/erikhaag/Neuvolics/frixon_symbol"),
            shadow = Assets.textures.atoms.shadow,
            matteTextures = new()
            {
                diffuse = AssetLoaderHelper.LoadTexture("textures/atoms/erikhaag/Neuvolics/frixon_diffuse"),
                shade = AssetLoaderHelper.LoadTexture("textures/atoms/erikhaag/Neuvolics/frixon_symbol"),
            }
        };
        gelaron = new()
        {
            symbol = AssetLoaderHelper.LoadTexture("textures/atoms/erikhaag/Neuvolics/gelaron_symbol"),
            shadow = Assets.textures.atoms.shadow,
            matteTextures = new()
            {
                diffuse = AssetLoaderHelper.LoadTexture("textures/atoms/erikhaag/Neuvolics/gelaron_diffuse"),
                shade = AssetLoaderHelper.LoadTexture("textures/atoms/erikhaag/Neuvolics/gelaron_symbol"),
            }
        };
        zephiron = new()
        {
            symbol = AssetLoaderHelper.LoadTexture("textures/atoms/erikhaag/Neuvolics/zephiron_symbol"),
            shadow = Assets.textures.atoms.shadow,
            matteTextures = new()
            {
                diffuse = AssetLoaderHelper.LoadTexture("textures/atoms/erikhaag/Neuvolics/zephiron_diffuse"),
                shade = AssetLoaderHelper.LoadTexture("textures/atoms/erikhaag/Neuvolics/zephiron_symbol"),
            }
        };
        hestium = new()
        {
            symbol = AssetLoaderHelper.LoadTexture("textures/atoms/erikhaag/Neuvolics/hestium_symbol"),
            shadow = Assets.textures.atoms.shadow,
            metallicTextures = {
                diffuse = Assets.textures.atoms.copper_diffuse,
                lightramp = AssetLoaderHelper.LoadTexture("textures/atoms/erikhaag/Neuvolics/hestium_lightramp"),
                rimlight = AssetLoaderHelper.LoadTexture("textures/atoms/erikhaag/Neuvolics/hestium_rimlight"),
            },
        };
        azulum = new()
        {
            symbol = AssetLoaderHelper.LoadTexture("textures/atoms/erikhaag/Neuvolics/azulum_symbol"),
            shadow = Assets.textures.atoms.shadow,
            metallicTextures = {
                diffuse = Assets.textures.atoms.copper_diffuse,
                lightramp = AssetLoaderHelper.LoadTexture("textures/atoms/erikhaag/Neuvolics/azulum_lightramp"),
                rimlight = AssetLoaderHelper.LoadTexture("textures/atoms/erikhaag/Neuvolics/azulum_rimlight"),
            },
        };
        taceum = new()
        {
            symbol = AssetLoaderHelper.LoadTexture("textures/atoms/erikhaag/Neuvolics/taceum_symbol"),
            shadow = Assets.textures.atoms.shadow,
            metallicTextures = {
                diffuse = Assets.textures.atoms.copper_diffuse,
                lightramp = AssetLoaderHelper.LoadTexture("textures/atoms/erikhaag/Neuvolics/taceum_lightramp"),
                rimlight = AssetLoaderHelper.LoadTexture("textures/atoms/erikhaag/Neuvolics/taceum_rimlight"),
            },
        };
        mitrum = new()
        {
            symbol = AssetLoaderHelper.LoadTexture("textures/atoms/erikhaag/Neuvolics/mitrum_symbol"),
            shadow = Assets.textures.atoms.shadow,
            metallicTextures = {
                diffuse = Assets.textures.atoms.copper_diffuse,
                lightramp = AssetLoaderHelper.LoadTexture("textures/atoms/erikhaag/Neuvolics/mitrum_lightramp"),
                rimlight = AssetLoaderHelper.LoadTexture("textures/atoms/erikhaag/Neuvolics/mitrum_rimlight"),
            },
        };
        iridium = new()
        {
            symbol = AssetLoaderHelper.LoadTexture("textures/atoms/erikhaag/Neuvolics/iridium_symbol"),
            shadow = Assets.textures.atoms.shadow,
            metallicTextures = {
                diffuse = Assets.textures.atoms.copper_diffuse,
                lightramp = AssetLoaderHelper.LoadTexture("textures/atoms/erikhaag/Neuvolics/iridium_lightramp"),
                rimlight = AssetLoaderHelper.LoadTexture("textures/atoms/erikhaag/Neuvolics/iridium_rimlight"),
            },
        };

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
