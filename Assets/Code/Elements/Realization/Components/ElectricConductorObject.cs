using CrazyWizardNotes.Elements.Abstractions;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Elements.Realization
{
    public sealed class ElectricConductorObject : IElectricConductor
    {
        private IActor Actor { get; set; }
        private IPhysical Physical { get; set; }
        private MaterialConfig Config { get; set; }

        public float ElectricCharge { get; set; }
        public float ElectricCapacity => Config.ElectricCapacity * Physical.Mass;
        public float ElectricResistance => Config.ElectricResistance;

        public event ElectricDischargeEventHandler OnElectricDischarge;

        public ElectricConductorObject(IActor actor, IPhysical physical, MaterialConfig config)
        {
            Actor = actor;
            Physical = physical;
            Config = config;
        }

        public void Discharge(float newCharge, float flowCharge)
        {
            var oldCharge = ElectricCharge;
            ElectricCharge = newCharge;

            OnElectricDischarge?.Invoke(new ElectricDischargeEventArgs(Actor, oldCharge, newCharge, flowCharge));
        }
    }
}
