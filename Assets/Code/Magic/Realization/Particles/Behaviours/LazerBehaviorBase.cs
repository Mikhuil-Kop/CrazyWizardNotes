using System.Collections.Generic;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Magic.Realization.Particles
{
    public abstract class LazerBehaviorBase : CustomParticleBehaviourBase
    {
        protected IPhysicsService PhysicsService { get; }
        protected ILazerVisualizer Visualizer { get; }
        protected IHasSpacePoint HasSpacePoint { get; }
        protected IPhysical Physical { get; }

        protected bool IsEnabled { get; set; }
        protected bool IsActive { get; set; }
        protected int Level { get; set; }

        public LazerBehaviorBase(IPhysicsService physicsService, ILazerVisualizer visualizer, IHasSpacePoint spacePoint, IPhysical physical)
        {
            PhysicsService = physicsService;
            Visualizer = visualizer;
            HasSpacePoint = spacePoint;
            Physical = physical;

            IsEnabled = false;
            IsActive = false;
            Level = 0;
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

                var actors = PhysicsService.GetActors(new PhysicsRaycastParams()
                {
                    From = HasSpacePoint.SpacePoint,
                    To = context.State.BendingTargetPoint.Value,
                    One = false
                });

                OnLazer(context, actors);
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

        protected abstract void OnLazer(ICustomParticleBehaviorContext context, IEnumerable<IActor> actors);
    }
}
