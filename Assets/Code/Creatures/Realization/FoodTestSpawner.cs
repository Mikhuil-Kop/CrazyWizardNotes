using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;
using UnityEngine;

namespace CrazyWizardNotes.Creatures.Realization
{
    public class FoodTestSpawner : IHandlesStart
    {
        public FoodTestSpawner(IAsyncJobService asyncJobService, IPhysicsService physicsService, IPhysicsActorSpawnService physicsActorSpawnService)
        {
            AsyncJobService = asyncJobService;
            PhysicsService = physicsService;
            PhysicsActorSpawnService = physicsActorSpawnService;
        }

        private IAsyncJobService AsyncJobService { get; }
        private IPhysicsService PhysicsService { get; }
        private IPhysicsActorSpawnService PhysicsActorSpawnService { get; }

        public void OnStart()
        {
            AsyncJobService.Register(() =>
            {
                PhysicsActorSpawnService.SpawnActor("Nature/Food", PhysicsService.GetDefaultSpacePoint(new UNet.Vector2D(Random.Range(-6f, 6f), Random.Range(-3, 3f))));
            }, 3);
        }
    }
}
