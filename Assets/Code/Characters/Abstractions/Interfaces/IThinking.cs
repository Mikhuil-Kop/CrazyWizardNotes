namespace CrazyWizardNotes.Characters.Abstractions
{
    /// <summary>
    /// Объект имеет мысли, которые можно прочесть.
    /// </summary>
    public interface IThinking
    {
        string GetCurrentThoughtDialog();
    }
}