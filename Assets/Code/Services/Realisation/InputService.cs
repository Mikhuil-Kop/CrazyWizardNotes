using System.Collections.Generic;
using System.Linq;
using CrazyWizardNotes.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CrazyWizardNotes.Common.Services
{
    public class InputService : SingletonBehavior<IInputService>, IInputService
    {
        [SerializeField]
        private Camera Camera;
        [SerializeField]
        private GraphicRaycaster Raycaster;
        [SerializeField]
        private EventSystem EventSystem;
        [SerializeField]
        private Dictionary<string, GameObject> Buttons;

        public Vector2 CameraPosition { get; private set; }

        public Vector2 Pointer { get; private set; }

        public Vector2 ScreenPointer { get; private set; }

        public ISubject[] PointedTargets { get; private set; }

        public ISubject[] ScreenPointedTargets { get; private set; }


        protected override void Awake()
        {
            base.Awake();

            PointedTargets = new ISubject[0];
            ScreenPointedTargets = new ISubject[0];
        }

        private void Update()
        {
            var ray = Camera.ScreenPointToRay(Input.mousePosition);
            var point = ray.GetPoint(-Camera.transform.position.z);
            var hit = Physics2D.Raycast(point, Vector2.zero);

            CameraPosition = Camera.transform.position;
            ScreenPointer = Input.mousePosition;
            Pointer = point;
            PointedTargets = hit.transform != null
                ? hit.transform.gameObject.GetComponents<ISubject>()
                : new ISubject[0];

            var results = new List<RaycastResult>();
            var pointerEventData = new PointerEventData(EventSystem);

            pointerEventData.position = Input.mousePosition;
            Raycaster.Raycast(pointerEventData, results);

            ScreenPointedTargets = results
                .SelectMany(x => x.gameObject.GetComponents<ISubject>())
                .Select(x => x as ISubject)
                .ToArray();
        }

        public bool GetButtonDown(string buttonName)
        {
            if (buttonName == "mouse")
            {
                return Input.GetMouseButtonDown(0);
            }
            else
            {
                return Input.GetButtonDown(buttonName);
            }
        }

        public bool GetButton(string buttonName)
        {
            if (buttonName == "mouse")
            {
                return Input.GetMouseButton(0);
            }
            else
            {
                return Input.GetButton(buttonName);
            }
        }

        public bool GetButtonUp(string buttonName)
        {
            if (buttonName == "mouse")
            {
                return Input.GetMouseButtonUp(0);
            }
            else
            {
                return Input.GetButtonUp(buttonName);
            }
        }
    }
}