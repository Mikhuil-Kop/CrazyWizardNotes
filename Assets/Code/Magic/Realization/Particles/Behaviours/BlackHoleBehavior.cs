using CrazyWizardNotes.Magic.Abstractions;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;
using UnityEngine;

namespace CrazyWizardNotes.Magic.Realization.Particles
{
    public sealed class BlackHoleBehavior : CustomParticleBehaviourBase
    {
        private IPhysicsService PhysicsService { get; }
        private IHasSpacePoint HasSpacePoint { get; }
        private ITouches Touches { get; }
        private Animator Animator { get; }

        private int Level { get; set; }

        private const float TimeToLevel = 3.0f;

        public BlackHoleBehavior(Animator animator, IPhysicsService physicsService, IHasSpacePoint spacePoint, IPhysical physical, ITouches touches)
        {
            PhysicsService = physicsService;
            HasSpacePoint = spacePoint;
            Touches = touches;
            Animator = animator;

            Level = -1;
        }

        public override void OnUpdate(ICustomParticleBehaviorContext context)
        {
            if (context.State.IsBending)
            {
                var targetLevel = Mathf.FloorToInt(context.State.SumBendTime / TimeToLevel);

                if (Level <= targetLevel)
                {
                    Level = targetLevel;
                    Animator.SetInteger("Level", Level + 1);
                }
            }
        }

        public override void OnFixedUpdate(ICustomParticleBehaviorContext context)
        {
            if (context.State.IsBending)
            {
                var force = Level switch
                {
                    2 => 4f,
                    1 => 2f,
                    0 => 1f,
                    _ => 0f,
                };

                PhysicsService.AddCircleGravity(HasSpacePoint.SpacePoint.Position, force);
            }

            if (!context.State.IsBending && context.State.EndBendCount > 0)
            {
                var force = 10f;
                PhysicsService.AddCircleGravity(HasSpacePoint.SpacePoint.Position, force);
            }
        }

        public override void OnBendEnd(ICustomParticleBehaviorContext context)
        {
            context.IsBendEnabled = false;
            Animator.SetBool("Released", true);

            Touches.OnTouch -= Touches_OnTouch;
            Touches.OnTouch += Touches_OnTouch;
        }

        private void Touches_OnTouch(OnTouchEventArgs obj)
        {
            foreach (var touch in obj.Touches.ActorsAs<IDestroyable>())
            {
                touch.Destroy();
            }

            foreach (var touch in obj.Touches.ActorsAs<IVanishable>())
            {
                touch.Vanish();
            }
        }
    }
}