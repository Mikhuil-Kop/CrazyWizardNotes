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

        public Vector2 GetDistance(ISpacePoint point1, ISpacePoint point2)
        {
            var targetSpace = GetCommonSpace(point1, point2);
            var pos1 = GetPosition(point1, targetSpace);
            var pos2 = GetPosition(point2, targetSpace);

            return pos1 - pos2;
        }

        public Vector2 GetPosition(ISpacePoint point, ISpaceContainer targetSpace)
        {
            var position = point.Position;

            var newPosition = point.ParentSpace?
                .ToRecursion(x => x.ParentSpace)
                .TakeWhile(x => x.ParentSpace != targetSpace)
                .Aggregate(position, (x, y) => y.TranslateToParentSpace(x));

            return newPosition ?? position;
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

        private ISpaceContainer GetCommonSpace(ISpacePoint point1, ISpacePoint point2)
        {
            var spaces1 = point1.ParentSpace.ToRecursion(x => x.ParentSpace).Reverse().ToArray();
            var spaces2 = point2.ParentSpace.ToRecursion(x => x.ParentSpace).Reverse().ToArray();

            var space = spaces1.ToParallel(spaces2).LastOrDefault(x => x.Item1 == x.Item2);

            return space.Item1 ?? point1.ParentSpace;
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