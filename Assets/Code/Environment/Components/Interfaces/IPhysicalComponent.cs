using CrazyWizardNotes.Spells.Logic;
using UnityEngine;

namespace CrazyWizardNotes.Environment.Components.Interfaces
{
    public interface IPhysicalComponent : IObjectComponent
    {
        float Mass { get; }
        void ApplyForce(Vector2 force);
        void ApplyImpulse(Vector2 force);
    }
}
