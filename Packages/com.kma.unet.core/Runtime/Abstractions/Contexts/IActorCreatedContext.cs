using System;

namespace UNet.Core.Abstractions
{
    public interface IActorCreatedContext
    {
        IScene Scene { get; }
        IActor Actor { get; }
        ILogger Logger { get; }
        IKeyedServiceProvider ServiceProvider { get; }
    }
}