using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using CrazyWizardNotes.Interface;

namespace CrazyWizardNotes.Inventory.OpenedBooks
{
    public abstract class OpenedBookBase : MonoBehaviour, IOpenedBook
    {
        #region Поля

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

        public virtual void SetBookData(BookData bookData)
        {
            RectTransform.anchoredPosition = Vector2.zero;

            Pages = bookData.Pages
                .Select(x =>
                {
                    var resource = Resources.Load<GameObject>("OpenedBookPages/" + x.Type);
                    var gameObject = Instantiate(resource, PagesParentTransform);
                    var page = gameObject.GetComponent<IOpenedBookPage>();

                    page.SetPageItemData(x);

                    return page;
                })
                .ToList();
        }

        public virtual BookData GetBookData()
        {
            var bookData = ScriptableObject.CreateInstance<BookData>();

            bookData.Pages = Pages
                .Select(x => x.GetPageData())
                .ToArray();

            return bookData;
        }

        #endregion

        #region Поведение объекта стола

        public void ToTop()
        {
            RectTransform.SetAsFirstSibling();
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

        #endregion
    }
}