namespace UNet.Core.Abstractions
{
    public interface IComponentsWrapper
    {
        object[] GetComponents(IUServiceProvider serviceProvider);
    }
}