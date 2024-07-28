using System.Threading.Tasks;

namespace UNet.Core.Abstractions
{
    public interface IHandlesLateUpdateAsync
    {
        Task OnLateUpdateAsync();
    }
}
