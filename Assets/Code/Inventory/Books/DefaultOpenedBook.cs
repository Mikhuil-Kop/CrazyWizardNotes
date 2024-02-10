using TMPro;
using UnityEngine;

namespace CrazyWizardNotes.Inventory.OpenedBooks
{
    public class DefaultOpenedBook : OpenedBookBase
    {
        [SerializeField]
        private TextMeshProUGUI TitleText;

        public override void SetBookData(BookData bookData)
        {
            base.SetBookData(bookData);
            TitleText.text = bookData.Parameters[0];
        }

        public override BookData GetBookData()
        {
            var bookData =  base.GetBookData();

            bookData.Parameters[0] = TitleText.text;

            return bookData;
        }
    }
}