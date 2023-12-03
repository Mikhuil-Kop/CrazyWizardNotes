namespace UNet.Physics.Abstractions
{
    public interface IHasSpaceObjectReadonly : IHasSpacePointReadonly
    {
        SpaceObject SpaceObject { get; }
    }
}
