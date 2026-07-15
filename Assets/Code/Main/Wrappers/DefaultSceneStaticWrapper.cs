using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CrazyWizardNotes.Main.Wrappers
{
    public class DefaultSceneStaticWrapper : TypedWrapperBase
    {
        public Camera Camera;
        public Canvas Canvas;
        public TextMeshProUGUI ActiveLog;
        public Transform FieldRootToSpawn;

        public UNet.UI.Realization.TooltipUIMonoBehaviour TooltipUIMonoBehaviour;
        public UNet.UI.Realization.TooltipCanvasMonoBehaviour TooltipCanvasMonoBehaviour;

        protected override IEnumerable<object> GetObjects()
        {
            return new object[]
            {
                Camera,
                Canvas,
                ActiveLog,
                FieldRootToSpawn,
                TooltipUIMonoBehaviour,
                TooltipCanvasMonoBehaviour,
                new UNet.Physics.Realization.RootTransformContainer(),
            };
        }

        protected override IEnumerable<Type> GetWrappedTypes()
        {
            return new Type[]
            {
                typeof(UNet.Core.Realization.ActorsSpawnService),
                typeof(UNet.Core.Realization.ActorsCollectionService),
                typeof(UNet.Core.Realization.AsyncExecutionService),
                typeof(UNet.Core.Realization.AsyncJobService),
                typeof(UNet.Core.Realization.CameraService),
                typeof(UNet.Core.Realization.DebugGizmosService),
                typeof(UNet.Core.Realization.EventsService),
                typeof(UNet.Core.Realization.SceneManagerService),
                typeof(UNet.Core.Realization.SchedulerService),
                typeof(UNet.Core.Realization.DebugLogger),
                typeof(UNet.Core.Realization.ResourcesService),

                typeof(UNet.Inputs.Realization.CameraInputService),

                typeof(UNet.Physics.Realization.PhysicsService),
                typeof(UNet.Physics.Realization.PhysicsActorSpawnService),

                typeof(UNet.Sound.Realization.SoundService),

                typeof(UNet.UI.Realization.LocalizationService),
                typeof(UNet.UI.Realization.TooltipService),

                //typeof(Magic.Realization.SpellBarService),
                //typeof(Magic.Realization.SpellBendService),
                //typeof(Magic.Realization.SpellParticleService),
                typeof(Electricity.Realization.PhysicsElectricService),

                typeof(UI.Realization.ActiveLogger),

                typeof(Creatures.Realization.NavigationService),
                typeof(Creatures.Realization.FoodTestSpawner),
                typeof(Nature.Realization.SimpleTerrainService)
            };
        }
    }
}
