using System;
using UNet.Core.Abstractions;

namespace UNet.Physics.Abstractions
{
    public interface IPhysicsActorSpawnService : IActorsSpawnService
    {
        IActor SpawnActor(string actorId, SpacePoint spawnPoint, Action<IActorCreateContext> onCreate = null);
    }
}