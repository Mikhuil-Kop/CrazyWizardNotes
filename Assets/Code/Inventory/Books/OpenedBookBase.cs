using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using CrazyWizardNotes.Interface;
using System.Xml.Linq;

namespace CrazyWizardNotes.Inventory.OpenedBooks
{
    public abstract class OpenedBookBase : MonoBehaviour, IOpenedBook
    {
        #region Поля

        [SerializeField]
        private string TypeName;
        [SerializeField]
        private RectTransform RectTransform;
        [SerializeField]
        private RectTransform PagesParentTransform;

        public string Value => "OpenedBook";

        public IDropper CurrentDropper { get; set; }

        public Vector2 Position
        {
            get => RectTransform.position;
            set => RectTransform.position = value;
        }

        private List<IOpenedBookPage> Pages { get; set; }

        #endregion

        #region Поведение книги


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
            if (Pages == null)
            {
                Pages = PagesParentTransform.GetComponentsInChildren<IOpenedBookPage>().ToList();
            }

            return new XElement("Book",
                new XAttribute("Type", TypeName),
                new XElement("Pages",
                    Pages.Select(page => page.GetInventoryData())));
        }

        public virtual void SetInventoryData(XElement data)
        {
            RectTransform.anchoredPosition = Vector2.zero;

            Pages = data.Element("Pages").Elements()
                .Select(x => InventoryService.Instance.LoadInventoryObject(x, PagesParentTransform) as IOpenedBookPage)
                .ToList();
        }

        #endregion
    }
}