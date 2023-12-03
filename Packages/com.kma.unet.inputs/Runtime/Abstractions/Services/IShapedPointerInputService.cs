namespace UNet.Inputs.Abstractions
{
    public interface IShapedPointerInputService : IPointerInputService
    {
        PointerShape PointerShape { get; }

        PointerShape ScreenPointerShape { get; }
    }
}