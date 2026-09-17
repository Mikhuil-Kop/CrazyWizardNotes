using System;
using UNet.Core.Abstractions;

namespace CrazyWizardNotes.Elements.Abstractions
{
    public class ElectricDischargeEventArgs : EventArgs
    {
        public IActor Actor { get; }
        public float OldElectricCharge { get; }
        public float NewElectricCharge { get; }
        public float DeltaElectricCharge { get; }
        public float FlowElectricCharge { get; }

        public ElectricDischargeEventArgs(IActor actor, float oldElectricCharge, float newElectricCharge, float flowElectricCharge)
        {
            Actor = actor;
            OldElectricCharge = oldElectricCharge;
            NewElectricCharge = newElectricCharge;
            DeltaElectricCharge = newElectricCharge - oldElectricCharge;
            FlowElectricCharge = flowElectricCharge;
        }
    }

    public delegate void ElectricDischargeEventHandler(ElectricDischargeEventArgs args);
}
