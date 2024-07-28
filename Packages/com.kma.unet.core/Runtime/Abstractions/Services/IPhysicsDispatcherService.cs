using System;

namespace UNet.Core.Abstractions
{
    public interface IPhysicsDispatcherService
    {
        void Invoke(Action action);
    }
}