using System.Threading.Tasks;
using UNet.Core.Abstractions;
using UnityEngine;

namespace UNet.Core.Realization
{
    public class ResourcesService : IResourcesService
    {
        public T Load<T>(string path) where T : class
        {
            Debug.Log("Loading " + path);

            var loaded = Resources.Load<Object>(path.Replace('\\', '/'));

            Debug.Log("Loading " + path + ": " + loaded);

            if (loaded is IResource<T> c)
            {
                return c.Value;
            }

            return loaded as T;
        }

        public Task<T> LoadAsync<T>(string path) where T : class
        {
            return Task.Run(() => Load<T>(path));
        }
    }
}