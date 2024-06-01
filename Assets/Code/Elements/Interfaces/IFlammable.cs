namespace CrazyWizardNotes.Elements
{
    public interface IFlammable
    {
        bool Burning { get; set; }
        float MaxFuelEnergy { get; set; }
        float CurrentFuelEnergy { get; set; }
        float FuelEnergyBurnoutSpeed { get; set; }
        float EnergyToStartBurning { get; set; }
    }
}
