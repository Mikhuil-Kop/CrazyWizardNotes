using System.Threading.Tasks;

namespace UNet.Core.Abstractions
{
    public interface IHandlesCustomLateUpdateAsync
    {
        Task OnLateUpdateAsync(ICustomUpdateContext context);
    }
}
