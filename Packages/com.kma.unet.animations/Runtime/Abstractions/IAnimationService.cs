namespace UNet.Animations.Abstractions
{
    public interface IAnimationService
    {
        void Trigger(string key);
        void SetParam(string key, int value);
        void SetParam(string key, float value);
        void SetParam(string key, bool value);
    }
}