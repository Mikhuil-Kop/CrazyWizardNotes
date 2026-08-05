namespace UNet.Core.Abstractions
{
    public interface IServicesWrapper
    {
        void AddServices(IUServiceProvider serviceProvider);
    }
}