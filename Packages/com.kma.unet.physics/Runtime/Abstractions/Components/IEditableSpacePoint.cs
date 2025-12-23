namespace UNet.Physics.Abstractions
{
    public interface IEditableSpacePoint : ISpacePoint
    {
        new ISpaceContainer ParentSpace { set; }
        new Vector2D Position { set; }
    }
}