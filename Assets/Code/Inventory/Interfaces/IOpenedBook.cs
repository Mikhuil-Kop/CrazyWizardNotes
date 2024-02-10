using System;

namespace CrazyWizardNotes.Inventory
{
    public interface IOpenedBook : ITableObject
    {
        BookData GetBookData();

        void SetBookData(BookData bookData);
    }
}