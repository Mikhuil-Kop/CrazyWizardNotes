namespace CrazyWizardNotes.Elements
{
    public delegate void OnEnergyChangedEventHandler(OnEnergyChangedEventArgs args);

    public class OnEnergyChangedEventArgs
    {
        public ElementsValues Before { get; set; }
        public ElementsValues After { get; set; }
    }
}

