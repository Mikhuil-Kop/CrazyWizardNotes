using System;
using System.Collections.Generic;
using UNet.Core.Abstractions;

namespace UNet.Physics.Abstractions
{
    public interface IPhysicsService
    {
        IEnumerable<IActor> GetActors(PhysicsConditionParams conditions);
        ISpacePoint GetDefaultSpacePoint(Vector2D vector2D);
        Vector2D GetPosition(ISpacePoint point, ISpaceContainer container);
        Vector2D GetDistance(ISpacePoint point1, ISpacePoint point2);
        void AddForce(PhysicsForceParams forceParams);
    }

    public static class PhysicsServiceExtensions
    {
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