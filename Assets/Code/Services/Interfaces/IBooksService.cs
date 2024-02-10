namespace CrazyWizardNotes.Common.Services
{
    public interface IBooksService
    {
        void CloseBook(string bookName);
        void OpenBook(string bookName);
    }
}