using System;
using System.Collections.Generic;
using System.Linq;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;
using UnityEngine;

namespace UNet.Physics.Realization
{
    public class PhysicsService : IPhysicsService//, IHandlesCustomUpdate
    {
        private IActorsCollectionService ActorsCollectionService { get; }
        private ISpaceChunkActorsFactory ChunkSpaceActorsFactory { get; }
        private IVirtualActorsService VirtualActorsService { get; }
        private ISpaceContainer RootSpaceContainer { get; }

        public PhysicsService(IActorsCollectionService subjectsCollectionService, ISpaceChunkActorsFactory chunkActorsFactory, IVirtualActorsService virtualActorsService)
        {
            ActorsCollectionService = subjectsCollectionService;
            ChunkSpaceActorsFactory = chunkActorsFactory;
            VirtualActorsService = virtualActorsService;

            RootSpaceContainer = new DefaultRootSpaceContainer(VirtualActorsService, ChunkSpaceActorsFactory, Vector2D.Zero, new Vector2D(1f, 1f));
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

        public IEnumerable<IActor> GetActors(PhysicsRaycastParams raycast)
        {
            var hits = raycast.One
                ? new[] { Physics2D.Raycast(raycast.From.Position, raycast.To.Position - raycast.From.Position) }
                : Physics2D.RaycastAll(raycast.From.Position, raycast.To.Position - raycast.From.Position);

            return hits.Select(x => x.collider?.gameObject?.GetComponent<IActor>()).Where(x => x != null);
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


        public IActor GetClosest(SpacePoint point, Func<IActor, bool> condition)
        {
            if (condition == null) throw new ArgumentNullException(nameof(condition));

            return ActorsCollectionService.GetAll()
                .Where(x => x.Is<IHasSpacePoint>())
                .Where(condition)
                .MinBy(x => GetDistance(point, x.As<IHasSpacePoint>().SpacePoint).MagnitudeSqr);
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

        //public float OnCustomUpdateDelay => 1f;
        //public void OnCustomUpdate()
        //{
        //    RecalculateSpaceChunks();
        //}

        //private void RecalculateSpaceChunks()
        //{
        //    //RootSpaceContainer.SpaceChunks.Resize(ActorsCollectionService.GetAll().ActorsAs<IHasSpacePoint>().Select(x => x.SpacePoint.Position));
        //}

        #region Внутренние классы

        public class DefaultRootSpaceContainer : ISpaceContainer
        {
            public ISpaceContainer ParentSpace => null;
            public ISpaceChunks SpaceChunks { get; }

            public DefaultRootSpaceContainer(IVirtualActorsService actorsService, ISpaceChunkActorsFactory factory, Vector2D worldOrigin, Vector2D chunkSize)
            {
                SpaceChunks = new SpaceChunks(actorsService, factory, this, worldOrigin, chunkSize);
            }

            public Vector2D TranslateToParentSpace(Vector2D vector)
            {
                throw new Exception($"Unable to get parent space vector. {nameof(DefaultRootSpaceContainer)} don`t have {nameof(ParentSpace)}.");
            }
        }

        #endregion Внутренние классы
    }
}