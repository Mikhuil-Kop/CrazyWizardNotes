namespace UNet.Core.Abstractions
{
    public interface IHandlesCustomUpdate
    {
        void OnUpdate(ICustomUpdateContext context);
    }
}
