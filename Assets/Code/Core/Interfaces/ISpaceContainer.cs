using UnityEngine;

namespace CrazyWizardNotes.Interfaces
{
    public interface ISpaceContainer
    {
        ISpaceContainer ParentSpace { get; set; }
        Vector2 TranslateToParentSpace(Vector2 vector2);
    }
}