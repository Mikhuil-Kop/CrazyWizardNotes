namespace CrazyWizardNotes.Common.Services
{
    public interface ILoadingRecource<T> where T : class
    {
        T Value { get; }
        bool Loaded { get; }
    }
}