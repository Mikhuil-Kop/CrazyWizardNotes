using System;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;

namespace UNet.Physics.Realization
{
    public class PhysicsActorSpawnService : IPhysicsActorSpawnService
    {
        private IActorsSpawnService ActorsSpawnService { get; }

        public PhysicsActorSpawnService(IActorsSpawnService actorsSpawnService)
        {
            ActorsSpawnService = actorsSpawnService;
        }

        public IActor SpawnActor(string actorId, SpacePoint spawnPoint, Action<IActorCreateContext> onCreate = null, Action<IActorCreatedContext> onCreated = null)
        {
            onCreated += x =>
            {
                var hasSpacePoint = x.ServiceProvider.GetRequiredService<IHasSpacePoint>();
                hasSpacePoint.SpacePoint = spawnPoint;
            };

            return ActorsSpawnService.SpawnActor(actorId, onCreate, onCreated);
        }
    }
}