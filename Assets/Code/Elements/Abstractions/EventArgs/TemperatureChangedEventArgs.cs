using System;
using UNet.Core.Abstractions;

namespace CrazyWizardNotes.Elements.Abstractions
{
    public class TemperatureChangedEventArgs : EventArgs
    {
        public IActor Actor { get; }
        public float OldTemperature { get; }
        public float NewTemperature { get; }
        public float DeltaTemperature { get; }
        public float OldEnergy { get; }
        public float NewEnergy { get; }
        public float DeltaEnergy { get; }

        public TemperatureChangedEventArgs(IActor actor, float oldTemperature, float newTemperature, float oldEnergy, float newEnergy)
        {
            Actor = actor;
            OldTemperature = oldTemperature;
            NewTemperature = newTemperature;
        }
    }

    public delegate void TemperatureChangedEventHandler(TemperatureChangedEventArgs e);
}
