using UNet.Core.Abstractions;
using UnityEngine;

namespace UNet.Core.Realization
{
    public class CameraService : ICameraService
    {
        private Camera Camera { get; }

        public Vector2D CameraPosition { get; private set; }

        public Vector2D Pointer { get; private set; }

        public Vector2D ScreenPointer { get; private set; }

        public IActor[] PointedTargets { get; private set; }

        public IActor[] ScreenPointedTargets { get; private set; }

        public CameraService(Camera camera)
        {
            Camera = camera;
            PointedTargets = new IActor[0];
            ScreenPointedTargets = new IActor[0];
        }

        public void OnUpdate(float deltaTime)
        {
            var ray = Camera.ScreenPointToRay(Input.mousePosition);
            var point = ray.GetPoint(-Camera.transform.position.z);
            var hit = Physics2D.Raycast(point, Vector2.zero);

            CameraPosition = Camera.transform.position;
            ScreenPointer = Input.mousePosition;
            Pointer = point;
            PointedTargets = hit.transform != null
                ? hit.transform.gameObject.GetComponents<IActor>()
                : new IActor[0];

            //var results = new List<RaycastResult>();

            //var pointerEventData = new PointerEventData(EventSystem)
            //{
            //    position = Input.mousePosition
            //};

            //Raycaster.Raycast(pointerEventData, results);

            //ScreenPointedTargets = results
            //    .SelectMany(x => x.gameObject.GetComponents<IActor>())
            //    .ToArray();
        }

        public Vector2D WorldToScreen(Vector2D origin)
        {
            return Camera.WorldToScreenPoint(origin);
        }

        public Vector2D ScreenToWorld(Vector2D origin)
        {
            return Camera.ScreenPointToRay(origin).origin;
        }
    }
}