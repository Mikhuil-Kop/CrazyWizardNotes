namespace CrazyWizardNotes.Reading.Abstractions
{
    public interface IReadSource
    {
        string GetReadText(IReadHandler reading);
    }
}
