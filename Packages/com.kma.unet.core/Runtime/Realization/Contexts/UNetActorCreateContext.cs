using UNet.Common;
using UNet.Core.Abstractions;
using ILogger = UNet.Core.Abstractions.ILogger;

namespace UNet.Core.Realization
{
    public class UNetActorCreateContext : IActorCreateContext
    {
        public IScene Scene { get; set; }
        public ILogger Logger { get; set; }
        public IReadonlyTags ActorTags { get; set; }
        public IUServiceProvider ServiceProvider { get; set; }
        public IActorComponentsProvider ComponentsProvider { get; set; }
    }
}