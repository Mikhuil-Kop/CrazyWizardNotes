using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace CrazyWizardNotes.Inventory.OpenedBookPages
{
    public abstract class OpenedBookPageBase : MonoBehaviour, IOpenedBookPage
    {
        [SerializeField]
        private RectTransform RectTransform;
        [SerializeField]
        private RectTransform ItemsParentTransform;

        private List<IOpenedBookPageItem> Items { get; set; }

        public virtual void SetPageItemData(PageData pageData)
        {
            RectTransform.anchoredPosition = Vector2.zero;

            Items = pageData.Items
                .Select(x =>
                {
                    var resource = Resources.Load<GameObject>("OpenedBookPageItems/" + x.Type);
                    var gameObject = Instantiate(resource, ItemsParentTransform);
                    var item = gameObject.GetComponent<IOpenedBookPageItem>();

                    item.SetPageItemData(x);

                    return item;
                })
                .ToList();
        }

        public virtual PageData GetPageData()
        {
            var pageData = new PageData();

            pageData.Items = Items
                .Select(x => x.GetPageItemData())
                .ToArray();

            return pageData;
        }
    }
}