using System;
using UNet.Core.Abstractions;

namespace CrazyWizardNotes.Elements.Abstractions
{
    public class MoistureChangedEventArgs : EventArgs
    {
        public IActor Actor { get; }
        public float OldMoisture { get; }
        public float NewMoisture { get; }
        public float DeltaMoisture { get; }
        public float OldMoisturePercentage { get; }
        public float NewMoisturePercentage { get; }
        public float DeltaMoisturePercentage { get; }

        public MoistureChangedEventArgs(IActor actor, float oldMoisture, float newMoisture, float oldMoisturePercentage, float newMoisturePercentage)
        {
            Actor = actor;
            OldMoisture = oldMoisture;
            NewMoisture = newMoisture;
            DeltaMoisture = newMoisture - oldMoisture;

            OldMoisturePercentage = oldMoisturePercentage;
            NewMoisturePercentage = newMoisturePercentage;
            DeltaMoisturePercentage = newMoisturePercentage - oldMoisturePercentage;
        }
    }

    public delegate void MoistureChangedEventHandler(MoistureChangedEventArgs e);
}
