using System;
using System.Threading.Tasks;

namespace CrazyWizardNotes.Common.Services
{
    public interface IAsyncLoadService
    {
        ILoadingRecource<T> Load<T>(Task<T> task, float timeToLoad, Action<T> onLoaded = null) where T : class;
    }
}