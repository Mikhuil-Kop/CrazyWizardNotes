namespace UNet.Core.Abstractions
{
    public interface IHandlesLateUpdate
    {
        void OnLateUpdate(float deltaTime);
    }
}
