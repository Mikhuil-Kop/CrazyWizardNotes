namespace UNet.Physics.Abstractions
{
    public interface IHasSpacePoint : IHasSpacePointReadonly
    {
        new SpacePoint SpacePoint { get; set; }
    }
}
