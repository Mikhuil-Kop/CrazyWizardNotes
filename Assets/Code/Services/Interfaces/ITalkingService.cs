using CrazyWizardNotes.Interfaces;

namespace CrazyWizardNotes.Services
{
    public interface ITalkingService
    {
        void StartDialog(ISubject subject, string dialogName);
    }
}