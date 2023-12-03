namespace CrazyWizardNotes.Elements.Abstractions
{
    public interface IElectricConductor
    {
        float ElectricCharge { get; }
        float ElectricCapacity { get; }
        float ElectricResistance { get; }
        event ElectricDischargeEventHandler OnElectricDischarge;

        void Discharge(float newCharge, float flowCharge);
    }
}
