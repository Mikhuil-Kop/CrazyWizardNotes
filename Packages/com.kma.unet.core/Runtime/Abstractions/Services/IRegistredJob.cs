using System;

namespace UNet.Core.Abstractions
{
    public interface IRegistredJob : IDisposable
    {
        void Cancel();
    }
}