using System;
using UNet.Core.Abstractions;

namespace UNet.Physics.Abstractions
{
    public interface IPhysicsActorSpawnService : IActorsSpawnService
    {
        void SpawnActor(string actorId, ISpacePoint spawnPoint, Action<IActorCreateContext> onCreate = null);
    }
}