using CrazyWizardNotes.Elements.Abstractions;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;
using UnityEngine;

namespace CrazyWizardNotes.Magic.Realization.Particles
{
    public class HeatwaveBehavior : CustomParticleBehaviourBase
    {
        private IPhysicsService PhysicsService { get; }
        private ILazerVisualizer Visualizer { get; }
        private IHasSpacePoint HasSpacePoint { get; }
        private IPhysical Physical { get; }

        private bool IsEnabled { get; set; }
        private bool IsActive { get; set; }
        private int Level { get; set; }

        public HeatwaveBehavior(IPhysicsService physicsService, ILazerVisualizer visualizer, IHasSpacePoint spacePoint, IPhysical physical)
        {
            PhysicsService = physicsService;
            Visualizer = visualizer;
            HasSpacePoint = spacePoint;
            Physical = physical;

            IsEnabled = false;
            IsActive = false;
        }

        public override void OnBendStart(ICustomParticleBehaviorContext context)
        {
            if (!IsEnabled)
            {
                IsEnabled = true;
                Visualizer.IsEnabled = true;
                return;
            }

            if (!IsActive)
            {
                IsActive = true;
                Visualizer.IsActive = true;
                return;
            }
        }

        public override void OnUpdate(ICustomParticleBehaviorContext context)
        {
            if (IsActive && context.State.BendingTargetPoint != null)
            {
                var dist = PhysicsService.GetDistance(HasSpacePoint, context.State.BendingTargetPoint.Value);
                var level = (int)(dist.Magnitude / 3f) + 1;

                if (Level != level)
                {
                    Level = level;
                    Visualizer.Level = level;
                }

                Visualizer.UpdateTarget(dist);
            }

            if (context.State.CurFreeTime > 3f)
            {
                context.IsVanishRequested = true;
            }
        }

        public override void OnFixedUpdate(ICustomParticleBehaviorContext context)
        {
            if (IsEnabled && !context.State.IsBending)
            {
                Physical.ApplyBreak(3f, context.DeltaTime);
            }

            if (IsActive && context.State.IsBending)
            {
                Physical.ApplyBreak(6f, context.DeltaTime);

                var heated = PhysicsService.GetActors(new PhysicsRaycastParams()
                {
                    From = HasSpacePoint.SpacePoint,
                    To = context.State.BendingTargetPoint.Value,
                    One = false
                });

                foreach (var actor in heated.WithComponents<IHasTemperature, IFlammable, IHasSpacePoint>())
                {
                    actor.Component1?.AddThermalEnergy(Level * 3f * context.DeltaTime);
                    actor.Component2?.ApplySpark(Level / 10f);

                    if (actor.Component3 != null)
                    {
                        Debug.DrawLine(HasSpacePoint.SpacePoint.Position, actor.Component3.SpacePoint.Position, Color.red);
                        Debug.Log($"Draw from {HasSpacePoint.SpacePoint.Position} to {actor.Component3.SpacePoint.Position}");
                    }
                }
            }
        }

        public override void OnBendEnd(ICustomParticleBehaviorContext context)
        {
            context.BendForceMult = 0f;

            if (IsActive)
            {
                IsActive = false;
                Visualizer.IsActive = false;

                context.IsVanishRequested = true;
            }

            if (Level > 0)
            {
                Level = 0;
                Visualizer.Level = 0;
            }
        }
    }
}
// Stopped
