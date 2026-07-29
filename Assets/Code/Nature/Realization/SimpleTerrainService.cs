using CrazyWizardNotes.Nature.Abstractions;
using UNet;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;
using UnityEngine;

namespace CrazyWizardNotes.Nature.Realization
{
    public class SimpleTerrainService : ITerrainService, IHandlesStart, IHandlesUpdate
    {
        public SimpleTerrainService(IPhysicsService physicsService, IPhysicsActorSpawnService physicsActorSpawnService)
        {
            PhysicsService = physicsService;
            PhysicsActorSpawnService = physicsActorSpawnService;
        }

        private IPhysicsService PhysicsService { get; set; }
        private IPhysicsActorSpawnService PhysicsActorSpawnService { get; set; }

        private string[] AssetNames { get; set; } = new string[]
        {
            "Nature/Food_0",
            "Nature/Food_1",
            "Nature/Food_2",
            "Nature/Food_3",

            "Nature/Grass_0",
            "Nature/Grass_1",
            "Nature/Grass_2",
            "Nature/Grass_3",
            "Nature/Grass_4",
            "Nature/Grass_5",

            "Nature/Tree_1",
            "Nature/Tree_2",
            "Nature/Tree_3",
            "Nature/Tree_4",
        };

        public void OnStart()
        {
            for (int i = 0; i < 50; i++)
            {
                var a = AssetNames[Random.Range(0, AssetNames.Length)];
                var x = Random.Range(-10f, 10f);
                var y = Random.Range(-5f, 5f);

                PhysicsActorSpawnService.SpawnActor(a, PhysicsService.GetDefaultSpacePoint(new Vector2D(x, y)));
            }
        }

        public void OnUpdate(float deltaTime)
        {
            
        }
    }
}
