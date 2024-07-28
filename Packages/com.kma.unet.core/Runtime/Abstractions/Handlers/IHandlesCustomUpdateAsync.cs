using System.Threading.Tasks;

namespace UNet.Core.Abstractions
{
    public interface IHandlesCustomUpdateAsync
    {
        Task OnUpdateAsync(ICustomUpdateContext context);
    }
}
