namespace UNet.Physics.Abstractions
{
    public interface IHasSpaceObject : IHasSpaceObjectReadonly, IHasSpacePoint
    {
        new ISpaceObject SpaceObject { get; set; }
    }
}
