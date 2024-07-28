using System.Threading.Tasks;

namespace UNet.Core.Abstractions
{
    public interface IResourcesService
    {
        T Load<T>(string path);
        Task<T> LoadAsync<T>(string path);
    }
}