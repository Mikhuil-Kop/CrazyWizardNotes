using System.Threading.Tasks;

namespace CrazyWizardNotes.Common.Services
{
    public interface IAsyncQueueService
    {
        void Invoke(Task task);
        void Invoke(Task task, int milliseconds);
    }
}