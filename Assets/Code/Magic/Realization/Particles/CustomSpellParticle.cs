using CrazyWizardNotes.Magic.Abstractions;
using UNet;
using UNet.Common;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;
using GameObject = UnityEngine.GameObject;
using Transform = UnityEngine.Transform;

namespace CrazyWizardNotes.Magic.Realization
{
    public class CustomSpellParticle : ISpellParticle, ISpellBendable, IHandlesFixedUpdate, IHandlesUpdate, IVanishable
    {
        #region Поля

        private IPhysicsService PhysicsService { get; }
        private IActor Actor { get; }
        private IPhysical Physical { get; }
        private IHasSpacePoint HasSpacePoint { get; }
        private Transform Transform { get; }
        private Turbulence Turbulence { get; }
        private CustomParticleBehaviourBase Behaviour { get; }
        private CustomParticleBehaviorContext Context { get; }

        public CustomSpellParticle(IPhysicsService physicsService, IActor actor, IHasSpacePoint hasSpacePoint, IPhysical physical, Transform transform, CustomParticleBehaviourBase behaviour)
        {
            PhysicsService = physicsService;
            
            Actor = actor;
            Physical = physical;
            HasSpacePoint = hasSpacePoint;
            Transform = transform;
            Behaviour = behaviour;

            Turbulence = new Turbulence();

            Context = new CustomParticleBehaviorContext()
            {
                ParticleActor = Actor,

                DeltaTime = 1f,
                BendForceMult = 1f,
                IsBendEnabled = true,

                State = new CustomParticleBehaviorState()
                {
                    IsBending = false,
                    StartBendCount = 0,
                    EndBendCount = 0,
                    BendingTargetPoint = null,

                    SumExistsTime = 0f,
                    CurBendTime = 0f,
                    SumBendTime = 0f,
                    CurFreeTime = 0f,
                    SumFreeTime = 0f,
                }
            };

            Context.State.Context = Context;
        }

        #endregion

        #region Функционал

        public bool IsBendEnabled => Context.IsBendEnabled;

        public void OnStartBend(SpellBendStartEventArgs args)
        {
            Context.State.BendingTargetPoint = args.StartPoint;
            Context.State.IsBending = true;
            Context.State.StartBendCount++;

            Context.State.CurBendTime = 0;
            Context.State.CurFreeTime = 0;

            Behaviour.OnBendStart(Context);
        }

        public void OnUpdateBend(SpellBendUpdateEventArgs args)
        {
            Context.State.BendingTargetPoint = args.TargetPoint;
            Context.State.IsBending = true;

            Behaviour.OnBendUpdate(Context);
        }

        public void OnEndBend(SpellBendEndEventArgs args)
        {
            Context.State.BendingTargetPoint = null;
            Context.State.IsBending = false;
            Context.State.EndBendCount++;

            Context.State.CurBendTime = 0;
            Context.State.CurFreeTime = 0;

            Behaviour.OnBendEnd(Context);
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            if (Context.State.IsBending && Context.State.BendingTargetPoint != null)
            {
                var dist = PhysicsService.GetDistance(Context.State.BendingTargetPoint.Value, HasSpacePoint);
                var distM = dist.Magnitude;
                //var damp = Math.Max(1 - distM * distM, 0.2f);
                var damp = 0.5f;
                var force3 = dist * distM * 30f;

                Physical.ApplyForce(force3 * Context.BendForceMult);
                Physical.ApplyForce(-Physical.SpeedSummary * (damp * 20f * Context.BendForceMult));
            }
            
            if(Context.State.StartBendCount <= 0)
            {
                var turbulence = Turbulence.Tick(fixedDeltaTime);
                Physical.ApplyForce(turbulence + Vector2D.Up * 0.1f);
            }

            Behaviour.OnFixedUpdate(Context);
        }

        public void OnUpdate(float deltaTime)
        {
            Context.State.SumExistsTime += deltaTime;

            if (Context.State.IsBending)
            {
                Context.State.SumBendTime += deltaTime;
                Context.State.CurBendTime += deltaTime;
                Context.State.CurFreeTime = 0f;
            }
            else
            {
                Context.State.SumFreeTime += deltaTime;
                Context.State.CurFreeTime += deltaTime;
                Context.State.CurBendTime = 0f;
            }

            if (Context.State.SumExistsTime >= 60f)
            {
                UnityEngine.Debug.LogWarning($"Particle {Transform.name} has existed for more than 60 seconds. Consider destroying it to avoid performance issues.");
            }

            Behaviour.OnUpdate(Context);
        }

        public void Vanish()
        {
            GameObject.Destroy(Transform.gameObject);
        }

        #endregion

        #region Внутренние классы

        private class CustomParticleBehaviorContext : ICustomParticleBehaviorContext
        {
            public IActor ParticleActor { get; internal set; }
            public CustomParticleBehaviorState State { get; internal set; }
            ICustomParticleBehaviorState ICustomParticleBehaviorContext.State => State;
            public float DeltaTime { get; internal set; }
            public float BendForceMult { get; set; }
            public bool IsBendEnabled { get; set; }
        }

        private class CustomParticleBehaviorState : ICustomParticleBehaviorState
        {
            internal CustomParticleBehaviorContext Context { get; set; }

            public bool IsBending { get; internal set; }
            public int StartBendCount { get; internal set; }
            public int EndBendCount { get; internal set; }
            public SpacePoint? BendingTargetPoint { get; internal set; }

            public float SumExistsTime { get; internal set; }
            public float CurBendTime { get; internal set; }
            public float SumBendTime { get; internal set; }
            public float CurFreeTime { get; internal set; }
            public float SumFreeTime { get; internal set; }
        }

        #endregion
    }
}
