using UnityEngine;

namespace CrazyWizardNotes.Inventory.OpenedBookPageItems
{
    public class OpenedBookPageItemBase : MonoBehaviour, IOpenedBookPageItem
    {
        [SerializeField]
        private RectTransform RectTransform;

        public virtual void SetPageItemData(PageItemData pageItemData)
        {
            RectTransform.anchoredPosition = pageItemData.Position;
            RectTransform.sizeDelta = pageItemData.Size;
        }

        public virtual PageItemData GetPageItemData()
        {
            var pageItemData = new PageItemData();

            pageItemData.Position = RectTransform.anchoredPosition;
            pageItemData.Size = RectTransform.sizeDelta;

            return pageItemData;
        }
    }
}