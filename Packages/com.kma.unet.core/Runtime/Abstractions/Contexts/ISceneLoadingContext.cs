namespace UNet.Core.Abstractions
{
    public interface ISceneLoadingContext
    {
        ISceneLoadingProcess SceneLoadingProcess { get; }
        bool PreventOpenNextScene { get; set; }
    }
}