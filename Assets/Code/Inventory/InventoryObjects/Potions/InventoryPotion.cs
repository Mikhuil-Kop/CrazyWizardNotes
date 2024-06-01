using System.Linq;
using System.Xml.Linq;
using CrazyWizardNotes.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace CrazyWizardNotes.Inventory
{
    public class InventoryPotion : MonoBehaviour, IInventoryObject
    {
        #region Поля

        [SerializeField]
        private string TypeName;
        [SerializeField]
        private Image Image;
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
            return false;
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
            return new XElement("Page",
                new XAttribute("Type", TypeName),
                new XElement("Volume", 1),
                Image.color.ToXml("Color"));
        }

        public virtual void SetInventoryData(XElement data)
        {
            RectTransform.anchoredPosition = Vector2.zero;
            Image.color = data.Element("Color").ParseColor();
        }

        #endregion
    }
}