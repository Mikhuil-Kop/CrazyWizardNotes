using UnityEngine;

namespace CrazyWizardNotes.Interfaces
{
    public interface ISpacePoint
    {
        ISpaceContainer ParentSpace { get; set; }
        Vector2 Position { get; }
    }
}