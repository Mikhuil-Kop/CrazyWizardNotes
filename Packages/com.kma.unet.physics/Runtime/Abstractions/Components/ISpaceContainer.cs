namespace UNet.Physics.Abstractions
{
    public interface ISpaceContainer
    {
        ISpaceContainer ParentSpace { get; }
        Vector2D TranslateToParentSpace(Vector2D vector);
    }
}