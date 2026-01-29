namespace UNet.Physics.Abstractions
{
    public interface IHasSpaceObjectReadonly : IHasSpacePointReadonly
    {
        ISpaceObject SpaceObject { get; }
    }
}
