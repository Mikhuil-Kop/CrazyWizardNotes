using System.Threading.Tasks;

namespace UNet.Core.Abstractions
{
    public interface IHandlesUpdateAsync
    {
        Task OnUpdateAsync(float deltaTime);
    }
}
