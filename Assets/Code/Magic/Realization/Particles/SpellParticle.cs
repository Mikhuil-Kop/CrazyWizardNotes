using CrazyWizardNotes.Magic.Abstractions;
using System.Collections.Generic;
using System.Linq;
using UNet;
using UNet.Common;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;
using Logger = UnityEngine.Debug;
using Transform = UnityEngine.Transform;
using GameObject = UnityEngine.GameObject;

namespace CrazyWizardNotes.Magic.Realization
{
    public class SpellParticle : ISpellParticle, ISpellBendable, IHandlesFixedUpdate, IVanishable
    {
        private IPhysicsService PhysicsService { get; }
        private IHasSpacePoint SpacePoint { get; }
        private IPhysical Physical { get; }
        private ITouches Touches { get; }
        private Transform Transform { get; }
        private Turbulence Turbulence { get; }
        private Dictionary<string, int> SpellWords { get; }
        private ISpacePoint BendingTargetPoint { get; set; }
        private bool WasBended { get; set; }
        private bool IsBending { get; set; }

        public SpellParticle(IPhysicsService physicsService, IHasSpacePoint spacePoint, IPhysical physical, ITouches touches, Transform transform, [FromKey("SpellWord")] string spellWord)
        {
            PhysicsService = physicsService;
            SpacePoint = spacePoint;
            Physical = physical;
            Touches = touches;
            Transform = transform;

            Turbulence = new Turbulence();

            SpellWords = new Dictionary<string, int>() { { spellWord, 1 } };
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

                MergeParticles(Touches.TouchingActors.ActorsAs<SpellParticle>().ToArray());
            }
            
            if(!WasBended)
            {
                var turbulence = Turbulence.Tick(fixedDeltaTime);
                Physical.ApplyForce(turbulence + Vector2D.Up * 0.1f);
            }
        }

        private void MergeParticles(params SpellParticle[] particles)
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
                    for (int i = 0; i < spellWord.Value; i++)
                    {
                        AddSpellWord(spellWord.Key);
                    }
                }

                particle.Vanish();
            }

            Logger.Log($"Merged particles into one with words: {string.Join(", ", SpellWords.Select(sw => $"{sw.Key} x{sw.Value}"))}");
        }

        private void AddSpellWord(string spell)
        {
            if (SpellWords.ContainsKey(spell))
            {
                SpellWords[spell]++;
            }
            else
            {
                SpellWords[spell] = 1;
            }
        }

        public void Vanish()
        {
            GameObject.Destroy(Transform.gameObject);
        }
    }
}
