namespace CrazyWizardNotes.Elements
{
    public interface IContainsEnergy
    {
        ElementsValues ContainedEnergy { get; }
        float EnergyTransfer { get; }

        event OnEnergyChangedEventHandler OnEnergyChanged;
        void AddEnergy(ElementsValues value);
    }
}
