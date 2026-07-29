using System;

namespace UNet.Core.Abstractions
{
    public interface IActorsSpawnService
    {
        IActor SpawnActor(string actorId, Action<IActorCreateContext> onCreate = null);
    }
}
