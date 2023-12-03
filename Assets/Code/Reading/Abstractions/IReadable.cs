namespace CrazyWizardNotes.Reading.Abstractions
{
    public interface IReadSource
    {
        ReadText GetReadText(IReadHandler reading);
    }
}
