namespace CrazyWizardNotes.Electricity.Abstractions
{

    public interface IElectricConductor
    {
        float ElectricCapacity { get; }
        float ElectricCharge { get; }

        void SetCharge(float chargeValue);

        event ElectricDischargeHandler OnElectricDischarge;
    }

    public delegate void ElectricDischargeHandler(float discharge);
}
