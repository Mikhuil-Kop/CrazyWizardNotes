using CrazyWizardNotes.Interfaces;
using UnityEngine;

namespace CrazyWizardNotes.Common.Services
{
    public interface IInputService
    {
        Vector2 CameraPosition { get; }
        Vector2 Pointer { get; }
        Vector2 ScreenPointer { get; }
        ISubject[] PointedTargets { get; }
        ISubject[] ScreenPointedTargets { get; }

        bool GetButtonDown(string buttonName);

        bool GetButton(string buttonName);

        bool GetButtonUp(string buttonName);
    }
}