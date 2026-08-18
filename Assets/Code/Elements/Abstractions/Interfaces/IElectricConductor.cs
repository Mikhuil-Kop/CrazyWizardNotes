namespace CrazyWizardNotes.Elements.Abstractions
{

    public interface IElectricConductor
    {
        float ElectricCharge { get; set; }
        float ElectricCapacity { get; }
        event ElectricDischargeEventHandler OnElectricDischarge;
    }
}
