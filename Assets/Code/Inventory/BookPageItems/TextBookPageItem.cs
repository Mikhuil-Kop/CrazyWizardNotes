using TMPro;
using UnityEngine;

namespace CrazyWizardNotes.Inventory.OpenedBookPageItems
{
    public class TextBookPageItem : OpenedBookPageItemBase
    {
        [SerializeField]
        private TextMeshProUGUI Text;

        public override void SetPageItemData(PageItemData pageItemData)
        {
            base.SetPageItemData(pageItemData);

            Text.text = pageItemData.Parameters[0];
        }

        public override PageItemData GetPageItemData()
        {
            var pageItemData = base.GetPageItemData();

            pageItemData.Parameters[0] = Text.text;

            return pageItemData;
        }
    }
}