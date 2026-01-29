namespace UNet.Core.Abstractions
{
    public interface IActorDestroyContext
    {
        IUServiceProvider ServiceProvider { get; }
        ILogger Logger { get; }
        IScene Scene { get; }
        IActor Actor { get; }
    }
}