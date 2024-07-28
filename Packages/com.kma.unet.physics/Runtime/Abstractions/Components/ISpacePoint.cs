namespace UNet.Physics.Abstractions
{
    public interface ISpacePoint
    {
        ISpaceContainer ParentSpace { get; }
        Vector2D Position { get; }
    }
}