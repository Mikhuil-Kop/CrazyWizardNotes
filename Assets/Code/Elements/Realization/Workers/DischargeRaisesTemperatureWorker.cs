using CrazyWizardNotes.Elements.Abstractions;
using System;

namespace CrazyWizardNotes.Elements.Realization
{
    public class DischargeRaisesTemperatureWorker : IDisposable
    {
        private IHasTemperature HasTemperature { get; }
        private IElectricConductor ElectricConductor { get; }

        public DischargeRaisesTemperatureWorker(IHasTemperature hasTemperature, IElectricConductor electricConductor)
        {
            HasTemperature = hasTemperature;
            ElectricConductor = electricConductor;

            ElectricConductor.OnElectricDischarge += ElectricConductor_OnElectricDischarge;
        }

        private void ElectricConductor_OnElectricDischarge(ElectricDischargeEventArgs args)
        {
            HasTemperature.ThermalEnergy += args.FlowElectricCharge * args.FlowElectricCharge * ElectricConductor.ElectricResistance;
        }

        public void Dispose()
        {
            ElectricConductor.OnElectricDischarge -= ElectricConductor_OnElectricDischarge;
        }
    }
}
