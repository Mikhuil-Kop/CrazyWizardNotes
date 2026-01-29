using System;
using System.Collections.Generic;
using System.Linq;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;

namespace UNet.Physics.Realization
{
    public class PhysicsService : IPhysicsService
    {
        private IActorsCollectionService ActorsCollectionService { get; }
        private ISpaceContainer RootSpaceContainer { get; }

        public PhysicsService(IActorsCollectionService subjectsCollectionService, ISpaceContainer rootSpaceContainer)
        {
            ActorsCollectionService = subjectsCollectionService;
            RootSpaceContainer = rootSpaceContainer;
        }

        // TODO: на проде брутфорс будет лагать. Пока пойдет.
        public IEnumerable<IActor> GetActors(PhysicsConditionParams conditions)
        {
            if (conditions == null)
            {
                throw new ArgumentNullException(nameof(conditions));
            }

            if (conditions.ShapeType == PhysicsShapeType.None)
            {
                return new IActor[0];
            }

            return ActorsCollectionService.GetAll()
                .Where(x => CheckSpacePointCondition(conditions, x.As<IHasSpacePoint>()))
                .ToArray();
        }

        private bool CheckSpacePointCondition(PhysicsConditionParams conditions, IHasSpacePoint obj)
        {
            if (obj == null)
            {
                return false;
            }

            var point = obj.SpacePoint;

            if (conditions.ShapeType == PhysicsShapeType.Square)
            {
                var distance = GetDistance(conditions.ShapeCenter, point);
                var size = conditions.ShapeSize / 2;

                return Math.Abs(distance.X) < Math.Abs(size.X)
                    && Math.Abs(distance.Y) < Math.Abs(size.Y);
            }
            else if (conditions.ShapeType == PhysicsShapeType.Circle)
            {
                var distance = GetDistance(conditions.ShapeCenter, point);
                var size = conditions.ShapeSize / 2;

                var nx = distance.X / size.X;
                var ny = distance.Y / size.Y;

                return (nx * nx + ny * ny) <= 1.0f;
            }
            else
            {
                return false;
            }
        }

        public Vector2D GetDistance(ISpacePoint point1, ISpacePoint point2)
        {
            if (point1 == null)
            {
                throw new ArgumentNullException(nameof(point1));
            }

            if (point2 == null)
            {
                throw new ArgumentNullException(nameof(point2));
            }

            var targetSpace = GetCommonSpace(point1, point2);
            var pos1 = GetPosition(point1, targetSpace);
            var pos2 = GetPosition(point2, targetSpace);

            return pos1 - pos2;
        }

        public Vector2D GetPosition(ISpacePoint point, ISpaceContainer targetSpace)
        {
            if (point == null)
            {
                throw new ArgumentNullException(nameof(point));
            }

            if (targetSpace == null)
            {
                throw new ArgumentNullException(nameof(targetSpace));
            }

            var position = point.Position;

            var newPosition = point.ParentSpace?
                .ToRecursion(x => x.ParentSpace)
                .TakeWhile(x => x != targetSpace)
                .Aggregate(position, (x, y) => y.TranslateToParentSpace(x));

            return newPosition ?? position;
        }

        public void AddForce(PhysicsForceParams forceParams)
        {
            if (forceParams.ForceSelector == null)
            {
                throw new ArgumentNullException(nameof(forceParams.ForceSelector));
            }

            var objects = ActorsCollectionService.GetAll()
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
            var spaces1 = point1.ParentSpace.ToRecursion(x => x.ParentSpace).Reverse();
            var spaces2 = point2.ParentSpace.ToRecursion(x => x.ParentSpace).Reverse();

            var space = spaces1.ToParallel(spaces2).LastOrDefault(x => x.Item1 == x.Item2);

            return space.Item1 ?? point1.ParentSpace;
        }

        [Obsolete("Функция вызывает вопросы по смыслу.")]
        public ISpacePoint GetDefaultSpacePoint(Vector2D vector2D)
        {
            return PhysicsTools.GetSpacePoint(RootSpaceContainer, vector2D);
        }
    }
}