namespace UNet.Core.Abstractions
{
    public interface IResource<T> where T : class
    {
        T Value { get; }
    }
}
