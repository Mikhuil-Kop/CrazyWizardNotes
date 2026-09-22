namespace UNet.Physics.Abstractions
{
    public interface IColliderData
    {
        T GetMetadata<T>() where T : class;
    }
}
