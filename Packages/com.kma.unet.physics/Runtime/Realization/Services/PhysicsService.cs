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

        public PhysicsService(IActorsCollectionService subjectsCollectionService, IChunkSpaceActorFactory chunkActorFactory)
        {
            ActorsCollectionService = subjectsCollectionService;

            RootSpaceContainer = new DefaultRootSpaceContainer(chunkActorFactory, Vector2D.Zero, new Vector2D(1f, 1f));
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

        public Vector2D GetDistance(SpacePoint point1, SpacePoint point2)
        {
            var targetSpace = GetCommonSpace(point1, point2);
            var pos1 = GetPosition(point1, targetSpace);
            var pos2 = GetPosition(point2, targetSpace);

            return pos1 - pos2;
        }

        public Vector2D GetPosition(SpacePoint point, ISpaceContainer targetSpace)
        {
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
                .Select(x => new { HasSpacePoint = x.As<IHasSpacePoint>(), Physical = x.As<IPhysical>() })
                .Where(x => x.HasSpacePoint != null && x.Physical != null);

            foreach (var obj in objects)
            {
                var position = obj.HasSpacePoint.SpacePoint.Position;
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

        private ISpaceContainer GetCommonSpace(SpacePoint point1, SpacePoint point2)
        {
            var spaces1 = point1.ParentSpace.ToRecursion(x => x.ParentSpace).Reverse();
            var spaces2 = point2.ParentSpace.ToRecursion(x => x.ParentSpace).Reverse();

            var space = spaces1.ToParallel(spaces2).LastOrDefault(x => x.Item1 == x.Item2);

            return space.Item1 ?? point1.ParentSpace;
        }

        [Obsolete("Функция вызывает вопросы по смыслу.")]
        public SpacePoint GetDefaultSpacePoint(Vector2D vector2D)
        {
            return new SpacePoint(RootSpaceContainer, vector2D);
        }

        #region Внутренние классы

        public class DefaultRootSpaceContainer : ISpaceContainer
        {
            public ISpaceContainer ParentSpace => null;
            public ISpaceChunks SpaceChunks { get; }

            public DefaultRootSpaceContainer(IChunkSpaceActorFactory factory, Vector2D worldOrigin, Vector2D chunkSize)
            {
                SpaceChunks = new SpaceChunks(this, factory, worldOrigin, chunkSize);
            }

            public Vector2D TranslateToParentSpace(Vector2D vector)
            {
                throw new Exception($"Unable to get parent space vector. {nameof(DefaultRootSpaceContainer)} don`t have {nameof(ParentSpace)}.");
            }
        }

        #endregion Внутренние классы
    }
}