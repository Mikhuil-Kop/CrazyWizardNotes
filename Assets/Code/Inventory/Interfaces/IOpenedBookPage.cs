namespace CrazyWizardNotes.Inventory
{
    public interface IOpenedBookPage
    {
        PageData GetPageData();

        void SetPageItemData(PageData pageData);
    }
}