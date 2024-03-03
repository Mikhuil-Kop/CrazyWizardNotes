using UnityEngine;

namespace CrazyWizardNotes.Interfaces
{
    public interface ISpaceObject : ISpacePoint
    {
        Vector2 Size { get; }
        float Rotation { get; }
    }
}
