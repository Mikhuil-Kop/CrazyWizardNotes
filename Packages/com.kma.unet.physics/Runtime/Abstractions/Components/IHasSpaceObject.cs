namespace UNet.Physics.Abstractions
{
    public interface IHasSpaceObject : IHasSpaceObjectReadonly, IHasSpacePoint
    {
        new SpaceObject SpaceObject { get; set; }
    }
}
