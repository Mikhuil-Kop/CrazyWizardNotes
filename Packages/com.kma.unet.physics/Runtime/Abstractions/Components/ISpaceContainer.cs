namespace UNet.Physics.Abstractions
{
    public interface ISpaceContainer
    {
        ISpaceContainer ParentSpace { get; }
        ISpaceChunks SpaceChunks { get; }
        Vector2D TranslateToParentSpace(Vector2D vector);
    }
}