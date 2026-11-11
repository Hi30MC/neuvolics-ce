using Quintessential;
using NeuvolicsUtil;
using Quintessential.Logging;

namespace Neuvolics;

public class Neuvolics : QuintessentialMod {
    public override string ModId => "neuvolics";
    public static Neuvolics Instance { get; }
    public const string LogPrefix = "Neuvolics: ";

    public override void Load() {
        Instance.Log.Log(LogLevel.Info, LogPrefix + "Loaded!");
    }

    public override void LoadContent()
    {
        Instance.Log.Log(LogLevel.Info, LogPrefix + "Initializing...");
        Atoms.LoadAtoms();
        Wheels.AddWheelTypes();
        Parts.AddPartTypes();
        Parts.EnableTriplex();
        Sounds.AddSounds();

        ExportManager.ExportAll();
    }
    public override void LoadCompatContent() { }
    public override void FinaliseContent() { }

    public override void PostLoad() { }
    public override void Unload() { }

}
