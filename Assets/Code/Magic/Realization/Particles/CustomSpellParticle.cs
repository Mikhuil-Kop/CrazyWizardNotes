using CrazyWizardNotes.Magic.Abstractions;
using CrazyWizardNotes.Magic.Realization.Particles;
using CrazyWizardNotes.UI.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using UNet;
using UNet.Common;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;
using GameObject = UnityEngine.GameObject;
using Transform = UnityEngine.Transform;
using Vectors = UnityEngine.Vector2;

namespace CrazyWizardNotes.Magic.Realization
{
    public class CustomSpellParticle : ISpellParticle, ISpellBendable, IHandlesFixedUpdate, IHandlesUpdate, IVanishable
    {
        private IActiveLogger Logger { get; }
        private IBehaviourSelector BehaviourSelector { get; }
        private IPhysicsService PhysicsService { get; }
        private IHasSpacePoint SpacePoint { get; }
        private IPhysical Physical { get; }
        private ITouches Touches { get; }
        private Transform Transform { get; }
        private Turbulence Turbulence { get; }
        private HashSet<string> SpellWords { get; }
        private SpacePoint BendingTargetPoint { get; set; }
        private bool WasBended { get; set; }
        private bool IsBending { get; set; }
        private bool RequiresUpdate { get; set; }
        private CustomParticleStateContext StateContext { get; set; }
        private CustomParticleBehaviourBase Behaviour { get; set; }
        private Vector2D LastVelocity { get; set; }

        public CustomSpellParticle(IActor actor, IActiveLogger logger, IBehaviourSelector selector, IPhysicsService physicsService, IHasSpacePoint spacePoint, IPhysical physical, ITouches touches, Transform transform, [FromKey("SpellWord")] string spellWord)
        {
            Logger = logger;
            BehaviourSelector = selector;
            PhysicsService = physicsService;
            SpacePoint = spacePoint;
            Physical = physical;
            Touches = touches;
            Transform = transform;

            RequiresUpdate = true;

            Turbulence = new Turbulence();

            SpellWords = new HashSet<string> { spellWord };

            StateContext = new CustomParticleStateContext()
            {
                Actor = actor,
                VelocityEnergyMult = 1,
                VelocityEnergyFade = 1,
                TorsionEnergyMult = 1,
                TorsionEnergyFade = 1
            };
        }

        public void OnStartBend(SpellBendStartEventArgs args)
        {
            BendingTargetPoint = args.StartPoint;
            IsBending = true;
            WasBended = true;
        }

        public void OnUpdateBend(SpellBendUpdateEventArgs args)
        {
            BendingTargetPoint = args.TargetPoint;
        }

        public void OnEndBend(SpellBendEndEventArgs args)
        {
            BendingTargetPoint = null;
            IsBending = false;
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            if (IsBending)
            {
                var dist = PhysicsService.GetDistance(BendingTargetPoint, SpacePoint);
                var distM = dist.Magnitude;
                //var damp = Math.Max(1 - distM * distM, 0.2f);
                var damp = 0.5f;
                var force3 = dist * distM * 30f;

                Physical.ApplyForce(force3);
                Physical.ApplyForce(-Physical.SpeedSummary * damp * 20f);

                MergeParticles(Touches.TouchingActors.ActorsAs<CustomSpellParticle>().ToArray());
            }
            
            if(!WasBended)
            {
                var turbulence = Turbulence.Tick(fixedDeltaTime);
                Physical.ApplyForce(turbulence + Vector2D.Up * 0.1f);
            }
        }

        public void OnUpdate(float deltaTime)
        {
            if (RequiresUpdate)
            {
                RequiresUpdate = false;

                var newBehaviour = BehaviourSelector.GetBehaviour(SpellWords);

                Behaviour = newBehaviour;
                StateContext.BehaviourTime = 0f;
            }

            UpdateState(deltaTime);
        }

        private void UpdateState(float deltaTime)
        {
            var curVelocity = Physical.SpeedSummary;
            var lastVelocity = LastVelocity;

            LastVelocity = curVelocity;
            
            var torsion = GetTorsion(deltaTime, curVelocity, lastVelocity);

            StateContext.DeltaTime = deltaTime;
            StateContext.ParticleLifeTime += deltaTime;
            StateContext.BehaviourTime += deltaTime;

            StateContext.Velocity = Physical.SpeedSummary;

            Behaviour.OnUpdate(StateContext);

            // Recalc
            
            StateContext.VelocityEnergyMult = 1;
            StateContext.VelocityEnergyFade = 1;
            StateContext.TorsionEnergyMult = 1;
            StateContext.TorsionEnergyFade = 1;

            StateContext.VelocityEnergy = Math.Max(0f, StateContext.VelocityEnergy + curVelocity.Magnitude * StateContext.VelocityEnergyMult * StateContext.DeltaTime * 0.5f - StateContext.VelocityEnergyFade * StateContext.DeltaTime * 20f);
            StateContext.TorsionEnergy = Math.Max(0f, StateContext.TorsionEnergy + torsion * StateContext.TorsionEnergyMult * StateContext.DeltaTime * 5f - StateContext.TorsionEnergyFade * StateContext.DeltaTime * 20f);

            // Logging
            var hash = GetHashCode();
            //Logger.LogActive(3f, $"{hash}: StateContext.DeltaTime", StateContext.DeltaTime.ToString("F2"));
            Logger.LogActive(3f, $"{hash}: StateContext.ParticleLifeTime", StateContext.ParticleLifeTime.ToString("F2"));
            Logger.LogActive(3f, $"{hash}: StateContext.BehaviourTime", StateContext.BehaviourTime.ToString("F2"));
            //Logger.LogActive(3f, $"{hash}: StateContext.Velocity", StateContext.Velocity.ToString());
            Logger.LogActive(3f, $"{hash}: StateContext.VelocityEnergy", StateContext.VelocityEnergy.ToString("F2"));
            //Logger.LogActive(3f, $"{hash}: StateContext.VelocityEnergyMult", StateContext.VelocityEnergyMult.ToString("F2"));
            //Logger.LogActive(3f, $"{hash}: StateContext.VelocityEnergyFade", StateContext.VelocityEnergyFade.ToString("F2"));
            Logger.LogActive(3f, $"{hash}: StateContext.TorsionEnergy", StateContext.TorsionEnergy.ToString("F2"));
            //Logger.LogActive(3f, $"{hash}: StateContext.TorsionEnergyMult", StateContext.TorsionEnergyMult.ToString("F2"));
            //Logger.LogActive(3f, $"{hash}: StateContext.TorsionEnergyFade", StateContext.TorsionEnergyFade.ToString("F2"));
            Logger.LogActive(3f, $"{hash}: Behaviour", Behaviour.GetType().Name);
        }

        private static float GetTorsion(float deltaTime, Vector2D curVelocity, Vector2D lastVelocity)
        {
            const float torsionFactor = 0.2f;
            const float minAngularSpeed = 15f;

            if (lastVelocity.MagnitudeSqrt > 0.01f && curVelocity.MagnitudeSqrt > 0.01f)
            {
                var lastDir = lastVelocity.Normalized;
                var currDir = curVelocity.Normalized;

                var angleChangeDeg = Vectors.SignedAngle(lastDir, currDir);
                var angularSpeedDegPerSec = angleChangeDeg / deltaTime;

                var absAngularSpeed = Math.Abs(angularSpeedDegPerSec);

                if (absAngularSpeed > minAngularSpeed)
                {
                    var add = absAngularSpeed * torsionFactor * deltaTime;
                    return add;
                }
            }

            return 0;
        }

        private void MergeParticles(params CustomSpellParticle[] particles)
        {
            var otherParticles = particles.Where(x => x != this).ToArray();

            if (otherParticles.Length == 0)
            {
                return;
            }

            foreach (var particle in otherParticles)
            {
                foreach (var spellWord in particle.SpellWords)
                {
                SpellWords.Add(spellWord);
                }

                particle.Vanish();
            }

            RequiresUpdate = true;

            Logger.LogInfo($"Merged particles into one with words: {string.Join(", ", SpellWords.Select(x => x))}");
        }

        public void Vanish()
        {
            GameObject.Destroy(Transform.gameObject);
        }
    }
}
