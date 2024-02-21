using UnityEngine;

namespace CrazyWizardNotes.Common.Services
{
    public interface IInputService
    {
        Vector2 CameraPosition { get; }
        Vector2 Pointer { get; }
        Vector2 ScreenPointer { get; }
        object[] PointedTargets { get; }
        object[] ScreenPointedTargets { get; }

        bool GetButtonDown(string buttonName);

        bool GetButton(string buttonName);

        bool GetButtonUp(string buttonName);
    }
}