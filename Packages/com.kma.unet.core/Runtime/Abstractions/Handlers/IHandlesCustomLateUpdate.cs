namespace UNet.Core.Abstractions
{
    public interface IHandlesCustomLateUpdate
    {
        void OnLateUpdate(ICustomUpdateContext context);
    }
}
