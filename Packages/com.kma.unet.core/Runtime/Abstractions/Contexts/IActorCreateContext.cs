namespace UNet.Core.Abstractions
{
    public interface IActorCreateContext
    {
        IUServiceProvider ServiceProvider { get; }
        ILogger Logger { get; }
        IScene Scene { get; }
        IActor Actor { get; }
    }
}