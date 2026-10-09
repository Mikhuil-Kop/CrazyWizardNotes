using System;
using System.Collections.Generic;
using UNet.Core.Abstractions;

namespace UNet.Physics.Abstractions
{
    public interface IPhysicsService
    {
        IEnumerable<IActor> GetActors(PhysicsConditionParams conditions);
        IEnumerable<IActor> GetActors(PhysicsRaycastParams raycast);
        SpacePoint GetDefaultSpacePoint(Vector2D vector2D);
        Vector2D GetPosition(SpacePoint point, ISpaceContainer container);
        Vector2D GetDistance(SpacePoint point1, SpacePoint point2);
        void AddForce(PhysicsForceParams forceParams);
    }

    public static class PhysicsServiceExtensions
    {
        public static Vector2D GetPosition(this IPhysicsService service, IHasSpacePoint point, ISpaceContainer container)
        {
            return service.GetPosition(point.SpacePoint, container);
        }

        public static Vector2D GetDistance(this IPhysicsService service, IHasSpacePoint point1, IHasSpacePoint point2)
        {
            return service.GetDistance(point1.SpacePoint, point2.SpacePoint);
        }

        public static Vector2D GetDistance(this IPhysicsService service, SpacePoint point1, IHasSpacePoint point2)
        {
            return service.GetDistance(point1, point2.SpacePoint);
        }

        public static Vector2D GetDistance(this IPhysicsService service, IHasSpacePoint point1, SpacePoint point2)
        {
            return service.GetDistance(point1.SpacePoint, point2);
        }

        public static void AddCircleGravity(this IPhysicsService service, Vector2D center, float force)
        {
            var forceParams = new PhysicsForceParams()
            {
                PhysicsForceType = PhysicsForceType.Force,
                ForceSelector = (x) =>
                {
                    var vec = center - x;
                    var mag = vec.Magnitude;

                    return mag < 0.01f
                        ? Vector2D.Zero
                        : (force / MathF.Min(MathF.Pow(mag, 2), 0.5f)) * vec;
                }
            };

            service.AddForce(forceParams);
        }






        #region Поиск

        private T FindClosest<T>(Func<IActor, T> selector, FindParams<T> param) where T : class
        {
            return ActorsCollectionService.GetAll()
                .Where(x => param.IncludeSelf || x != Creature)
                .Select(x => new
                {
                    Actor = x,
                    Value = selector(x)
                })
                .Where(x => param.Predicate?.Invoke(x.Value) ?? true)
                .OrderBy(x => PhysicsService.GetDistance(x.Actor.As<IHasSpacePoint>(), HasSpacePoint).Magnitude)
                .FirstOrDefault()?
                .Value;
        }

        private struct FindParams<T> where T : class
        {
            public Func<T, bool> Predicate { get; set; }
            public bool IncludeSelf { get; set; }
        }

        #endregion
    }
}