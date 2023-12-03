using UNet.Common;

namespace UNet.Core.Abstractions
{
    public interface IActorCreateContext
    {
        IScene Scene { get; }
        ILogger Logger { get; }
        IReadonlyTags ActorTags { get; }
        IUServiceProvider ServiceProvider { get; }
        IActorComponentsProvider ComponentsProvider { get; }
    }
}