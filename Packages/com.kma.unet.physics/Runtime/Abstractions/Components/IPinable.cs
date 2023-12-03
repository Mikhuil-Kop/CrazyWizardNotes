using System;

namespace UNet.Physics.Abstractions
{
    /// <summary>
    /// Объект можно закрепить в пространстве.
    /// </summary>
    public interface IPinable
    {
        event Action OnPin;
        event Action OnUnpin;

        void Pin(float forceToUnpin);
        void Unpin();
    }
}
