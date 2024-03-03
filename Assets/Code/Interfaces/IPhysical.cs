using UnityEngine;

namespace CrazyWizardNotes.Interfaces
{
    public interface IPhysical
    {
        float Mass { get; }
        Vector2 ForceSummary { get; }
        Vector2 SpeedSummary { get; }
        void ApplyForce(Vector2 force);
        void ApplyImpulse(Vector2 force);
    }
}
