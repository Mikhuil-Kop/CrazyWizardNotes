using UNet.Core.Abstractions;

namespace UNet.Core.Realization
{
    public class UNetApplicationCreateContext : IApplicationCreateContext
    {
        public IUServiceProvider ServiceProvider { get; internal set; }
    }
}
