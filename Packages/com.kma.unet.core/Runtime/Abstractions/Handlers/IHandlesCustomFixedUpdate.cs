namespace UNet.Core.Abstractions
{
    public interface IHandlesCustomFixedUpdate
    {
        void OnFixedUpdate(ICustomUpdateContext context);
    }
}
