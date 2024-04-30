using System;
using System.Threading.Tasks;

namespace CrazyWizardNotes.Common.Services
{
    public class AsyncLoadService : SingletonBehavior<IAsyncLoadService>, IAsyncLoadService
    {
        public ILoadingRecource<T> Load<T>(Task<T> task, float timeToLoad, Action<T> onLoaded = null) where T : class
        {
            var scheduleService = SchedulerService.Instance;
            var asyncQueueService = AsyncQueueService.Instance;

            var loadingResource = new LoadingRecource<T>()
            {
                Loaded = false,
                Value = null
            };

            scheduleService.Shedule(timeToLoad, () =>
            {
                asyncQueueService.Invoke(Task.Run(async () =>
                {
                    loadingResource.Value = await task;
                    loadingResource.Loaded = true;
                }));
            });

            return loadingResource;
        }

        public class LoadingRecource<T> : ILoadingRecource<T> where T : class
        {
            public T Value { get; set; }
            public bool Loaded { get; set; }
        }
    }
}