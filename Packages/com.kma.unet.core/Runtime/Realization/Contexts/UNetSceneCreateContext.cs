using UNet.Core.Abstractions;

namespace UNet.Core.Realization
{
    public class UNetSceneCreateContext : ISceneCreateContext
    {
        public IUServiceProvider ServiceProvider { get; internal set; }
        public ILogger Logger { get; internal set; }
        public IScene Scene { get; internal set; }

    }
}
