using UnityEngine;

namespace CrazyWizardNotes.Interfaces
{
    public interface ISpacePoint
    {
        ISpaceContainer ParentSpace { get; set; }
        Vector2 Position { get; }
    }

    public interface ISpaceContainer
    {
        ISpaceContainer ParentSpace { get; set; }
        Vector2 TranslateToParentSpace(Vector2 vector2);
    }
}