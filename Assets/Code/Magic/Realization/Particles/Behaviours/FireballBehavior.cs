using CrazyWizardNotes.Elements.Abstractions;
using System;
using System.Linq;
using UNet;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;
using UnityEngine;

namespace CrazyWizardNotes.Magic.Realization.Particles
{
    public class FireballBehavior : CustomParticleBehaviourBase
    {
        private IPhysicsService PhysicsService { get; }
        private IHasSpacePoint HasSpacePoint { get; }
        private IPhysical Physical { get; }
        private ITouches Touches { get; }
        private Animator Animator { get; }

        private float MaxSpeed { get; set; }
        private bool Exploded { get; set; }

        public FireballBehavior(Animator animator, IPhysicsService physicsService, IHasSpacePoint spacePoint, IPhysical physical, ITouches touches)
        {
            PhysicsService = physicsService;
            HasSpacePoint = spacePoint;
            Physical = physical;
            Touches = touches;
            Animator = animator;

            MaxSpeed = 0f;
        }

        public override void OnBendStart(ICustomParticleBehaviorContext context)
        {
            Animator.SetBool("Started", true);
        }

        public override void OnUpdate(ICustomParticleBehaviorContext context)
        {
            Debug.Log($"Spell - {nameof(context.State.WasBended)}: {context.State.WasBended};{nameof(context.State.IsBending)}: {context.State.IsBending};{nameof(context.State.WasReleased)}: {context.State.WasReleased};");

            // До управления
            if (!context.State.WasBended)
            {
                Debug.Log($"Spell - {nameof(context.State.CurFreeTime)}: {context.State.CurFreeTime};");
                if (context.State.CurFreeTime > 10f)
                {
                    context.IsVanishRequested = true;
                }
            }
            // Во время управления
            else if (context.State.IsBending)
            {
                if (Physical.SpeedSummary.Magnitude > MaxSpeed)
                {
                    MaxSpeed = Physical.SpeedSummary.Magnitude;
                    Animator.SetFloat("MaxSpeed", MaxSpeed);
                }
            }
            // После управления
            else if (context.State.WasReleased)
            {
                Debug.Log($"Spell - {nameof(context.State.CurFreeTime)}: {context.State.CurFreeTime}; {nameof(Physical.SpeedSummary.Magnitude)}: {Physical.SpeedSummary.Magnitude}");
                if (context.State.CurFreeTime > 10f || Physical.SpeedSummary.Magnitude <= 0.1f)
                {
                    Explode();
                }
            }

            if (Exploded)
            {
                context.IsVanishRequested = true;
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
            Explode();
        }

        private void Explode()
        {
            if (Exploded)
            {
                return;
            }

            Exploded = true;
            
            Animator.SetBool("Explode", true);

            Physical.ApplyImpulse(-Physical.SpeedSummary);
            Physical.ApplyForce(-Physical.ForceSummary);
            var size = 3;

            var targetActors = PhysicsService.GetActors(new PhysicsConditionParams()
            {
                ShapeCenter = HasSpacePoint.SpacePoint,
                ShapeSize = new Vector2D(size, size),
                ShapeType = PhysicsShapeType.Circle,
            }).ToArray();

            foreach (var actor in targetActors.WithComponents<IHasSpacePoint, IHasTemperature, IFlammable>())
            {
                var distance = 1f;

                if (actor.Component1 != null)
                {
                    distance = PhysicsService.GetDistance(HasSpacePoint, actor.Component1).Magnitude;
                }

                var speedMult = Math.Clamp(MaxSpeed, 0f, 100f);
                var distanceMult = 1 / Math.Clamp(distance, 1f, 10f);

                if (actor.Component2 != null)
                {
                    actor.Component2.ThermalEnergy += 2000f * speedMult * distanceMult;
                }

                if (actor.Component3 != null)
                {
                    actor.Component3.ApplySpark(speedMult * distanceMult);
                }
            }
        }
    }
}
// Stopped
