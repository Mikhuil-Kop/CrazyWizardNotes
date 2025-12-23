using System;

namespace UNet.Core.Abstractions
{
    public interface IActorsSpawnService
    {
        void SpawnActor(string actorId, Action<IActorCreateContext> onCreate = null);
    }
}
