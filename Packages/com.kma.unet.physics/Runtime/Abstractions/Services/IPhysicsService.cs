using System;
using System.Collections.Generic;
using UNet.Core.Abstractions;

namespace UNet.Physics.Abstractions
{
    public interface IPhysicsService
    {
        IEnumerable<IActor> GetActors(PhysicsConditionParams conditions);
        IEnumerable<IActor> GetActors(PhysicsRaycastParams raycast);

        IActor GetClosest(SpacePoint point, Func<IActor, bool> condition);

        SpacePoint GetDefaultSpacePoint(Vector2D vector2D);
        Vector2D GetPosition(SpacePoint point, ISpaceContainer container);
        Vector2D GetDistance(SpacePoint point1, SpacePoint point2);
        void AddForce(PhysicsForceParams forceParams);
    }

    public static class PhysicsServiceExtensions
    {
        public static IActor GetClosest(this IPhysicsService service, IActor actor, Func<IActor, bool> condition)
        {
            if (service == null) throw new ArgumentNullException(nameof(service));
            if (actor == null) throw new ArgumentNullException(nameof(actor));
            if (condition == null) throw new ArgumentNullException(nameof(condition));

            var hasSpacePoint = actor.As<IHasSpacePoint>();

            if (hasSpacePoint == null) return null;

            return service.GetClosest(hasSpacePoint.SpacePoint, x => x != actor && condition(x));
        }

        public static Vector2D GetPosition(this IPhysicsService service, IHasSpacePoint point, ISpaceContainer container)
        {
            if (service == null) throw new ArgumentNullException(nameof(service));
            if (point == null) throw new ArgumentNullException(nameof(point));

            return service.GetPosition(point.SpacePoint, container);
        }

        public static Vector2D GetDistance(this IPhysicsService service, IHasSpacePoint point1, IHasSpacePoint point2)
        {
            if(point1 == null) throw new ArgumentNullException(nameof(point1));
            if(point2 == null) throw new ArgumentNullException(nameof(point2));

            return service.GetDistance(point1.SpacePoint, point2.SpacePoint);
        }

        public static Vector2D GetDistance(this IPhysicsService service, SpacePoint point1, IHasSpacePoint point2)
        {
            if(point2 == null) throw new ArgumentNullException(nameof(point2));

            return service.GetDistance(point1, point2.SpacePoint);
        }

        public static Vector2D GetDistance(this IPhysicsService service, IHasSpacePoint point1, SpacePoint point2)
        {
            if(point1 == null) throw new ArgumentNullException(nameof(point1));

            return service.GetDistance(point1.SpacePoint, point2);
        }

        public static Vector2D? GetDistanceOrNull(this IPhysicsService service, IHasSpacePoint point1, IHasSpacePoint point2)
        {
            if(point1 == null || point2 == null) return null;

            return service.GetDistance(point1.SpacePoint, point2.SpacePoint);
        }

        public static Vector2D? GetDistanceOrNull(this IPhysicsService service, SpacePoint? point1, IHasSpacePoint point2)
        {
            if(point1 == null || point2 == null) return null;

            return service.GetDistance(point1.Value, point2.SpacePoint);
        }

        public static Vector2D? GetDistanceOrNull(this IPhysicsService service, IHasSpacePoint point1, SpacePoint? point2)
        {
            if(point1 == null || point2 == null) return null;

            return service.GetDistance(point1.SpacePoint, point2.Value);
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
    }
}