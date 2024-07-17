using CrazyWizardNotes.Core;
using CrazyWizardNotes.Interfaces;
using UnityEngine;

namespace CrazyWizardNotes.Common.Services
{
    public interface IPhysicsService
    {
        Vector2 GetPosition(ISpacePoint point, ISpaceContainer container);
        Vector2 GetDistance(ISpacePoint point1, ISpacePoint point2);

        void AddForce(PhysicsForceParams forceParams);
    }
}