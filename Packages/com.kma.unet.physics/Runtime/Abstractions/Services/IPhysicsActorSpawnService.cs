using System;
using UNet.Core.Abstractions;

namespace UNet.Physics.Abstractions
{
    public interface IPhysicsActorSpawnService
    {
        IActor SpawnActor(string actorId, SpacePoint spawnPoint, Action<IActorCreateContext> onCreate = null, Action<IActorCreatedContext> onCreated = null);
    }
}