namespace CrazyWizardNotes.Common
{
    public interface IRealisationProvider
    {
        T Get<T>() where T : class;
    }
}