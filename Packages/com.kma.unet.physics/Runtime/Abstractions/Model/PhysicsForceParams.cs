using System;
using UNet.Core.Abstractions;

namespace UNet.Physics.Abstractions
{
    public class PhysicsForceParams
    {
        public IPhysical[] Targets { get; set; }
        public Func<IActor, bool> TargetsFilter { get; set; }
        public PhysicsForceType PhysicsForceType { get; set; }
        public Func<Vector2D, Vector2D> ForceSelector { get; set; }
    }
}
