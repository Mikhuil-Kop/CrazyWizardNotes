using CrazyWizardNotes.Elements.Abstractions;

namespace CrazyWizardNotes.Elements.Realization
{
    public sealed class MagneticObject : IMagnetic
    {
        private MaterialConfig Config { get; }
        public float MagneticCharge { get; set; }
        public float MagneticSusceptibility => Config.MagneticSusceptibility;
        public bool IsAffectedByMagnetic => true;

        public MagneticObject(MaterialConfig config)
        {
            Config = config;
        }
    }
}
