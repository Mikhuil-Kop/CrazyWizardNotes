namespace UNet.Physics.Abstractions
{
    public interface ISpaceObject : ISpacePoint
    {
        Vector2D Size { get; }
        float Rotation { get; }
    }
}
