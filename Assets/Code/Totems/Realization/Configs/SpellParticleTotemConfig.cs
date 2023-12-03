using System;

namespace CrazyWizardNotes.Totems.Realization
{
    [Serializable]
    public class SpellParticleTotemConfig
    {
        public string SpellId;
        public float ActivationTimeGap;
        public bool IsEnabled;
        public bool IsActive;
    }
}
