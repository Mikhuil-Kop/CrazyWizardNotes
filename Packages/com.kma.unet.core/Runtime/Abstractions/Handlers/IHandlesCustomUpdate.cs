namespace UNet.Core.Abstractions
{
    public interface IHandlesCustomUpdate
    {
        float OnCustomUpdateDelay { get; }
        void OnCustomUpdate();
    }
}
