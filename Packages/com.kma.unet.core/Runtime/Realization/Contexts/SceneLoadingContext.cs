using UNet.Core.Abstractions;

namespace UNet.Core.Realization
{
    internal class SceneLoadingContext : ISceneLoadingContext
    {
        public ISceneLoadingProcess SceneLoadingProcess { get; internal set; }
        public bool PreventOpenNextScene { get; set; }
    }
}
