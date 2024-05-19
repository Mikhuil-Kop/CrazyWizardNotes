using CrazyWizardNotes.Core;
using UnityEngine;
using System.Linq;
using CrazyWizardNotes.Interfaces;

namespace CrazyWizardNotes.Common.Services
{
    public class PhysicsService : SingletonBehavior<IPhysicsService>, IPhysicsService
    {
        private ISubjectsCollectionService SubjectsCollectionService { get; set; }

        protected override void Awake()
        {
            base.Awake();

            SubjectsCollectionService = Locate<ISubjectsCollectionService>();
        }

        public void AddForce(PhysicsForceParams forceParams)
        {
            var objects = SubjectsCollectionService.GetAll()
                .Select(x => new { SpacePoint = x.As<ISpacePoint>(), Physical = x.As<IPhysical>() })
                .Where(x => x.SpacePoint != null && x.Physical != null);

            foreach (var obj in objects)
            {
                var position = obj.SpacePoint.Position;
                var force = forceParams.ForceSelector(position);

                if (forceParams.PhysicsForceType == PhysicsForceType.Force)
                {
                    obj.Physical.ApplyForce(force);
                }
                else
                {
                    obj.Physical.ApplyImpulse(force);
                }
            }
        }
    }

    public static class PhysicsServiceExtensions
    {
        public static void AddCircleGravity(this IPhysicsService service, Vector2 center, float force)
        {
            var forceParams = new PhysicsForceParams()
            {
                PhysicsForceType = PhysicsForceType.Force,
                ForceSelector = (x) =>
                {
                    var vec = center - x;
                    var mag = vec.magnitude;

                    return mag < 0.01f
                        ? Vector2.zero
                        : (force / Mathf.Min(Mathf.Pow(mag, 3), 0.5f)) * vec;
                }
            };

            service.AddForce(forceParams);
        }
    }
}