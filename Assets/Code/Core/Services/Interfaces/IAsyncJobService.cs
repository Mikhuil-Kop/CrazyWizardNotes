using System.Threading.Tasks;
using CrazyWizardNotes.Core.Model;

namespace CrazyWizardNotes.Core.Services
{
    public interface IAsyncJobService
    {
        void Register(AsyncJobParams job);
    }
}