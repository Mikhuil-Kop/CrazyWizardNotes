using System;

namespace UNet.Physics.Abstractions
{
    public interface IBreakable
    {
        event Action OnBreak;

        void Break();
    }
}
