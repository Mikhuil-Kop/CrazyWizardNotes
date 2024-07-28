using System;

namespace UNet.Core.Abstractions
{
    public interface IDispatcherService
    {
        void Invoke(Action action);
    }
}