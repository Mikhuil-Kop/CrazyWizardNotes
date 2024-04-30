using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrazyWizardNotes.Interface
{
    public class Dragger : MonoBehaviour, IDragger
    {
        [SerializeField]
        private RectTransform Rect;

        public string Value { get; set; } = "Code";

        public IDropper CurrentDropper { get; set; }

        public Vector2 Position
        {
            get => Rect.position;
            set => Rect.position = value;
        }

        public void ToTop()
        {
            Rect.SetAsLastSibling();
        }

        public bool CanDrag()
        {
            return true;
        }

        public bool CanDrop(IDropper dropper)
        {
            return true;
        }

        public void OnDrop(IDropper dropper)
        {
            Debug.Log("Подключились");
        }
    }

    public interface IDragger
    {
        string Value { get; }
        Vector2 Position { get; set; }
        IDropper CurrentDropper { get; set; }

        void ToTop();
        bool CanDrag();
        bool CanDrop(IDropper dropper);
        void OnDrop(IDropper dropper);
    }
}