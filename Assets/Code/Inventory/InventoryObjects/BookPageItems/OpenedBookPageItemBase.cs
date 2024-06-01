using System.Xml.Linq;
using UnityEngine;
using CrazyWizardNotes.Interface;

namespace CrazyWizardNotes.Inventory.OpenedBookPageItems
{
    public class OpenedBookPageItemBase : MonoBehaviour, IOpenedBookPageItem
    {
        #region Поля

        [SerializeField]
        private string TypeName;
        [SerializeField]
        private RectTransform RectTransform;

        public string Value => "BookPage";

        public Vector2 Position
        {
            get => RectTransform.position;
            set => RectTransform.position = value;
        }

        public IDropper CurrentDropper { get; set; }

        #endregion

        #region Поведение инвентаря

        public void ToTop()
        {
            RectTransform.SetAsLastSibling();
        }

        public bool CanDrag()
        {
            return true;
        }

        public bool CanDrop(IDropper dropper)
        {
            return false;
        }

        public void OnDrop(IDropper dropper)
        {
        }

        public virtual XElement GetInventoryData()
        {
            var rect = new Rect(RectTransform.anchoredPosition, RectTransform.sizeDelta);

            return new XElement("Item",
                new XAttribute("Type", TypeName),
                rect.ToXml());
        }

        public virtual void SetInventoryData(XElement data)
        {
            var rect = data.Element("Rect").ParseRect();

            RectTransform.anchoredPosition = rect.position;
            RectTransform.sizeDelta = rect.size;
        }

        #endregion
    }
}