using UnityEngine;

namespace CrazyWizardNotes.Common.Services
{
    public class CameraService : SingletonBehavior<ICameraService>, ICameraService
    {
        [SerializeField]
        private Camera Camera;

        public Vector2 WorldToScreen(Vector2 origin)
        {
            return Camera.WorldToScreenPoint(origin);
        }

        public Vector2 ScreenToWorld(Vector2 origin)
        {
            return Camera.ScreenPointToRay(origin).origin;
        }
    }
}