using UNet.Core.Abstractions;

namespace UNet.Core.Realization
{
    public class UNetStartupContext : IStartupContext
    {
        public IUAppBuilder AppBuilder { get; internal set; }
        public IUSceneBuilder SceneBuilder { get; internal set; }
        public IUActorBuilder ActorBuilder { get; internal set; }
        public IUServiceBuilder ServiceBuilder { get; internal set; }
    }
}
