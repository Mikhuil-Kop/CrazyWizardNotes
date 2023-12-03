namespace UNet.Core.Abstractions
{
    public interface IActorDestroyedContext
    {
        IUServiceProvider ServiceProvider { get; }
        ILogger Logger { get; }
        IScene Scene { get; }
        IActor Actor { get; }
    }
}