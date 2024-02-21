using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using CrazyWizardNotes.Interface;
using System.Xml.Linq;
using System.Security.Cryptography;

namespace CrazyWizardNotes.Inventory.OpenedBookPages
{
    public abstract class OpenedBookPageBase : MonoBehaviour, IOpenedBookPage
    {
        #region Поля

        [SerializeField]
        private string TypeName;
        [SerializeField]
        private RectTransform RectTransform;
        [SerializeField]
        private RectTransform ItemsParentTransform;

        public string Value => "BookPage";

        public Vector2 Position
        {
            get => RectTransform.position;
            set => RectTransform.position = value;
        }

        public IDropper CurrentDropper { get; set; }

        private List<IOpenedBookPageItem> Items { get; set; }

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
            if (Items == null)
            {
                Items = ItemsParentTransform.GetComponentsInChildren<IOpenedBookPageItem>().ToList();
            }

            return new XElement("Page",
                new XAttribute("Type", TypeName),
                new XElement("Items",
                    Items.Select(item => item.GetInventoryData())));
        }

        public virtual void SetInventoryData(XElement data)
        {
            RectTransform.anchoredPosition = Vector2.zero;

            Items = data.Element("Items").Elements()
                .Select(x => InventoryService.Instance.LoadInventoryObject(x, ItemsParentTransform) as IOpenedBookPageItem)
                .ToList();
        }

        #endregion
    }
}