using CrazyWizardNotes.Elements.Abstractions;
using System;
using System.Linq;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Elements.Realization
{
    public class MagneticService : IMagneticService, IHandlesFixedUpdate
    {
        private IActorsCollectionService ActorsCollectionService { get; }
        private IPhysicsService PhysicsService { get; }

        public MagneticService(IActorsCollectionService actorsCollectionService, IPhysicsService physicsService)
        {
            ActorsCollectionService = actorsCollectionService;
            PhysicsService = physicsService;
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            var actors = ActorsCollectionService
                .GetAll()
                .WithComponents<IHasSpacePoint, IMagnetic, IPhysical>()
                .Where(x => x.AllComponentsPresent)
                .Where(x => IsMagnetic(x.Component2))
                .ToArray();

            // Упрощенка

            foreach (var actor in actors.Where(x => x.Component2.IsAffectedByMagnetic))
            {
                var force = actors
                    .Where(x => x != actor)
                    .Select(x => new
                    {
                        distance = PhysicsService.GetDistance(x.Component1, actor.Component1),
                        magnetic = GetMagneticForce(x.Component2, actor.Component2)
                    })
                    .Sum(x => -x.distance.Normalized * (x.magnetic / Math.Max(x.distance.MagnitudeSqr, 1f)));

                actor.Component3.ApplyForce(force);
            }
        }

        private static bool IsMagnetic(IMagnetic magnetic)
        {
            return Math.Abs(magnetic.MagneticCharge) > 0.001f
                || Math.Abs(magnetic.MagneticSusceptibility) > 0.001f;
        }

        private static float GetMagneticForce(IMagnetic m1, IMagnetic m2)
        {
            var q1 = m1.MagneticCharge;
            var q2 = m2.MagneticCharge;

            // Постоянные магниты: знак = полярность (+ отталкивание, − притяжение)
            var chargeForce = q1 * q2;

            // Мягкое тело в поле магнита: F ~ χ B², всегда притяжение
            var inducedForce =
                m2.MagneticSusceptibility * q1 * q1 +
                m1.MagneticSusceptibility * q2 * q2;

            return chargeForce - inducedForce;
        }
    }
}
