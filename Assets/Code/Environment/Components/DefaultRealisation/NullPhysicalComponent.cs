using CrazyWizardNotes.Environment.Components.Interfaces;
using UnityEngine;

namespace CrazyWizardNotes.Environment.Components.DefaultRealisation
{
    public class NullPhysicalComponent : IPhysicalComponent
    {
        public NullPhysicalComponent()
        {
            Debug.Log("123212312");
        }

        public float Mass => 1;

        public void ApplyForce(Vector2 force)
        {
        }

        public void ApplyImpulse(Vector2 force)
        {
        }
    }
}