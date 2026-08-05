using System;
using UNet.Core.Abstractions;
using ILogger = UNet.Core.Abstractions.ILogger;

namespace UNet.Core.Realization
{
    public class UNetActorCreatedContext : IActorCreatedContext
    {
        public IScene Scene { get; set; }
        public IActor Actor { get; set; }
        public ILogger Logger { get; set; }
        public IKeyedServiceProvider ServiceProvider { get; set; }
    }
}