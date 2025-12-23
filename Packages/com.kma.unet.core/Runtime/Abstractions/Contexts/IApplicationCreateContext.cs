namespace UNet.Core.Abstractions
{
    public interface IApplicationCreateContext
    {
        IUServiceProvider ServiceProvider { get; }
    }
}