namespace UNet.Physics.Abstractions
{
    public interface IHasSpacePoint : IHasSpacePointReadonly
    {
        new ISpacePoint SpacePoint { get; set; }
    }
}
