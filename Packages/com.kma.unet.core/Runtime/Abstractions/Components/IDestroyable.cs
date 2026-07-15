using System;

namespace UNet.Core.Abstractions
{
    public interface IDestroyable
    {
        bool IsDestroyed { get; }

        event Action OnDestroy;
        void Destroy();
    }
}
