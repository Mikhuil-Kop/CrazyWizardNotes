using System;
using UnityEngine;

namespace Main.Interface
{
    public class DragAndDropManager : MonoBehaviour
    {
        public static DragAndDropManager Instance { get; set; }
        public Camera Camera;
        public Dragger CurrentDragger;

        public void Awake()
        {
            Instance = this;
        }

        public void Drag(Dragger dragger)
        {
            CurrentDragger = dragger;
        }

        public void Drop(Dropper dropper)
        {
            CurrentDragger = null;
            CurrentDragger.transform.position = dropper.transform.position;
        }

        public void DragAndDrop(Dragger dragger, Dropper dropper)
        {
            Drag(dragger);
            Drop(dropper);
        }

        public void Update()
        {
            var position = Camera.ScreenToWorldPoint(Input.mousePosition);
            position.z = -1;

            if (CurrentDragger != null)
            {
                CurrentDragger.transform.position = position;
            }
        }

        public void OnDestroy()
        {
            if (Instance == null)
            {
                return;
            }
        }
    }
}

