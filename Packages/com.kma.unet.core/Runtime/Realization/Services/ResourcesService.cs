using System.Threading.Tasks;
using UNet.Core.Abstractions;
using UnityEngine;

namespace UNet.Core.Realization
{
    public class ResourcesService : IResourcesService
    {
        public T Load<T>(string path) where T : class
        {
            return Resources.Load<Object>(path.Replace('\\', '/')) as T;
        }

        public Task<T> LoadAsync<T>(string path) where T : class
        {
            return Task.Run(() => Load<T>(path));
        }
    }
}