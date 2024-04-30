namespace CrazyWizardNotes.Interfaces
{
    public interface ISubject
    {
        bool Is<T>() where T : class;
        T As<T>() where T : class;
    }
}
