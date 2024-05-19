using System;
using UnityEngine;

namespace CrazyWizardNotes.Core
{
    public class PhysicsForceParams
    {
        public PhysicsForceType PhysicsForceType { get; set; }
        public Func<Vector2, Vector2> ForceSelector { get; set; }
    }

    public enum PhysicsForceType : byte
    {
        Force = 0,
        Impulse = 1
    }
}
