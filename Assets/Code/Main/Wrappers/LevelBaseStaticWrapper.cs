using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CrazyWizardNotes.Main.Wrappers
{
    public class LevelBaseStaticWrapper : TypedWrapperBase
    {
        public Camera Camera;
        public Canvas Canvas;
        public TextMeshProUGUI ActiveLog;
        public Transform FieldRootToSpawn;

        public UNet.Core.Realization.ActorSpawnServiceConfig ActorSpawnServiceConfig;
        public UNet.UI.Realization.TooltipUIMonoBehaviour TooltipUIMonoBehaviour;
        public UNet.UI.Realization.TooltipCanvasMonoBehaviour TooltipCanvasMonoBehaviour;

        public UI.Realization.ActionsBarServiceConfig ActionBarServiceConfig;
        public Scenes.Realization.SceneLoadAnimatorConfig SceneLoadAnimatorConfig;

        [Header("Эти данные используются, когда сцена была открыта через редактор, а не через SceneLoadService.")]
        public Scenes.Realization.SceneResource TestSceneResource;
        public Scenes.Abstractions.SceneRuntimeData TestRuntimeData;

        protected override IEnumerable<object> GetObjects()
        {
            return new object[]
            {
                Camera,
                Canvas,
                ActiveLog,
                FieldRootToSpawn,
                ActorSpawnServiceConfig,
                TooltipUIMonoBehaviour,
                TooltipCanvasMonoBehaviour,
                ActionBarServiceConfig,
                SceneLoadAnimatorConfig,
                new UNet.Physics.Realization.RootTransformContainer(),

                new UNet.Core.Realization.SceneLoadService.SceneLoadServiceConfig()
                {
                    TestRuntimeData = TestRuntimeData,
                    TestSceneResource = TestSceneResource,
                }
            };
        }

        protected override IDictionary<string, object> GetKeyedObjects()
        {
            return new Dictionary<string, object>
            {
                { "TestRuntimeData", TestRuntimeData },
                { "TestSceneResource", TestSceneResource },
            };
        }

        protected override IEnumerable<Type> GetWrappedTypes()
        {
            return new Type[]
            {
                typeof(Assets.Code.Canvas),

                typeof(UNet.Core.Realization.ActorsCollectionService),
                typeof(UNet.Core.Realization.AsyncExecutionService),
                typeof(UNet.Core.Realization.AsyncJobService),
                typeof(UNet.Core.Realization.CameraService),
                typeof(UNet.Core.Realization.DebugGizmosService),
                typeof(UNet.Core.Realization.EventsService),
                typeof(UNet.Core.Realization.DebugLogger),
                typeof(UNet.Core.Realization.ResourcesService),
                typeof(UNet.Core.Realization.ActorsSpawnService),
                typeof(UNet.Core.Realization.SceneLoadService),

                typeof(UNet.Inputs.Realization.CameraInputService),

                typeof(UNet.Physics.Realization.PhysicsService),
                typeof(UNet.Physics.Realization.PhysicsActorSpawnService),

                typeof(UNet.Sound.Realization.SoundService),

                typeof(UNet.UI.Realization.LocalizationService),
                //typeof(UNet.UI.Realization.TooltipService), // Работает, но надо включить позже

                typeof(Scenes.Realization.SceneLoadAnimator),
                typeof(Scenes.Realization.SceneRulesService),

                typeof(Magic.Realization.SpellBendService),
                typeof(Magic.Realization.SpellParticleService),
                typeof(Magic.Realization.SpellParticleFromSoundService),
                typeof(Magic.Realization.SpellWizardService),

                typeof(Elements.Realization.PhysicsElectricService),

                typeof(UI.Realization.ActiveLogger),
                typeof(UI.Realization.ActionBarService),

                typeof(Creatures.Realization.NavigationService),
                //typeof(Creatures.Realization.FoodTestSpawner),
                //typeof(Nature.Realization.SimpleTerrainService)
            };
        }
    }
}
