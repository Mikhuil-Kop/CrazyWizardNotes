namespace UNet.Core.Abstractions
{
    public interface ISceneCreateContext
    {
        IUServiceProvider ServiceProvider { get; }
        ILogger Logger { get; }
        IScene Scene { get; }
    }
}