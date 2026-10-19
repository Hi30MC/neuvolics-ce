using Quintessential;

namespace Neuvolics;

public class Neuvolics : QuintessentialMod {
    public override string ModId => "neuvolics";
    public static Neuvolics Instance { get; }
    public const string LogPrefix = "Neuvolics: ";

    public override void Load() {
        Logger.Log(LogPrefix + "Loaded!");
    }

    public override void LoadContent()
    {
        Logger.Log(LogPrefix + "Initializing...");
        Atoms.LoadAtoms();
        Wheels.AddWheelTypes();
        Parts.AddPartTypes();
        Parts.EnableTriplex();
        Sounds.AddSounds();

    }
    public override void LoadCompatContent() { }
    public override void FinaliseContent() { }

    public override void PostLoad() { }
    public override void Unload() { }

}
