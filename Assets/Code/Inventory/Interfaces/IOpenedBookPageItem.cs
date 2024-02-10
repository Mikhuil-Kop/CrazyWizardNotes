namespace CrazyWizardNotes.Inventory
{
    public interface IOpenedBookPageItem
    {
        PageItemData GetPageItemData();

        void SetPageItemData(PageItemData pageItemData);
    }
}