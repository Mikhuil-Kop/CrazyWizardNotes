using UnityEngine;

namespace CrazyWizardNotes.Common.Services
{
    public interface ICameraService
    {
        Vector2 ScreenToWorld(Vector2 origin);
        Vector2 WorldToScreen(Vector2 origin);
    }
}