using CrazyWizardNotes.Elements.Abstractions;
using System;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Magic.Realization.Particles
{
    public sealed class ArcWhipBehavior : CustomParticleBehaviourBase
    {
        private IPhysicsService PhysicsService { get; }
        private IHasSpacePoint SpacePoint { get; }
        private ITouches Touches { get; }
        private IArcWhipVisualizer Visualizer { get; }

        private bool SearchesForConnection { get; set; }
        private bool ConnectedToTarget { get; set; }
        private IPhysical TargetPhysical { get; set; }
        private IMagnetic TargetMagnetic { get; set; }
        private IHasSpacePoint TargetSpacePoint { get; set; }

        private const float FreeRadius = 3f;
        private const float PullForce = 10f;

        public ArcWhipBehavior(IPhysicsService physicsService, IHasSpacePoint spacePoint, ITouches touches, IArcWhipVisualizer visualizer)
        {
            PhysicsService = physicsService;
            SpacePoint = spacePoint;
            Touches = touches;
            Visualizer = visualizer;

            Touches.OnTouch += ConnectOnTouch;
        }

        public override void OnBendStart(ICustomParticleBehaviorContext context)
        {
            SearchesForConnection = false;
        }

        public override void OnUpdate(ICustomParticleBehaviorContext context)
        {
            if (context.State.WasBended && context.State.CurFreeTime > 5f
            || !context.State.WasBended && context.State.CurFreeTime > 10f)
            {
                context.IsVanishRequested = true;
            }
        }

        public override void OnFixedUpdate(ICustomParticleBehaviorContext context)
        {
            if (ConnectedToTarget)
            {
                var distance = PhysicsService.GetDistance(SpacePoint, TargetSpacePoint);

                Visualizer.UpdateTarget(distance, FreeRadius, context.State.IsBending);

                if (distance.Magnitude < FreeRadius)
                {
                    return;
                }

                if (!context.State.IsBending)
                {
                    return;
                }

                var maxM = FreeRadius * 2;
                var pullM = distance.Magnitude - FreeRadius;
                var magnet = TargetMagnetic.MagneticSusceptibility;
                var force = PullForce * Math.Max(pullM * pullM * pullM, maxM * maxM * maxM) * magnet;

                TargetPhysical.ApplyForce(force * distance.Normalized);
            }
        }

        public override void OnBendEnd(ICustomParticleBehaviorContext context)
        {
            if (!ConnectedToTarget)
            {
                SearchesForConnection = true;
                ConnectToTouching();
            }
        }


        private void ConnectOnTouch(OnTouchEventArgs obj)
        {
            if (SearchesForConnection)
            {
                foreach (var t in obj.Touched)
                {
                    if (TryConnect(t))
                    {
                        break;
                    }
                }
            }
        }

        private void ConnectToTouching()
        {
            foreach (var t in Touches.TouchingActors)
            {
                if (TryConnect(t))
                {
                    break;
                }
            }
        }

        private bool TryConnect(IActor actor)
        {
            var physical = actor.As<IPhysical>();
            var magnetic = actor.As<IMagnetic>();
            var spacePoint = actor.As<IHasSpacePoint>();

            if (physical == null || magnetic == null || spacePoint == null || magnetic.MagneticSusceptibility < 0.2f)
            {
                return false;
            }

            SearchesForConnection = false;
            ConnectedToTarget = true;

            TargetPhysical = physical;
            TargetMagnetic = magnetic;
            TargetSpacePoint = spacePoint;

            return true;
        }
    }
}