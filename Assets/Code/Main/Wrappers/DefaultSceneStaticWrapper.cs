using System;
using System.Collections.Generic;
using UNet.Core.Abstractions;
using UnityEngine;

namespace CrazyWizardNotes.Main.Wrappers
{
    public class DefaultSceneStaticWrapper : TypedWrapperBase, IOptionsWrapper
    {
        public Camera Camera;

        public object[] GetOptions()
        {
            return new object[]
            {
                Camera,
                new UNet.Physics.Realization.RootTransformContainer(),
            };
        }

        public override IEnumerable<Type> GetWrappedTypes()
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

                typeof(UNet.Inputs.Realization.CameraInputService),

                typeof(UNet.Physics.Realization.PhysicsService),
                typeof(UNet.Physics.Realization.PhysicsActorSpawnService),

                typeof(UNet.Sound.Realization.SoundService),

                typeof(Magic.Realization.SpellBendService),
                typeof(Magic.Realization.SpellParticleService),
            };
        }
    }
}
