namespace UNet.Animations.Abstractions
{
    public interface IHasAnimationData<T> where T : class
    {
        T AnimationValue { get; }
    }
}
