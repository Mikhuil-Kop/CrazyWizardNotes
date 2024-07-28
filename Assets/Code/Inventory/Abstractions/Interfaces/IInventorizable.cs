using System;

namespace CrazyWizardNotes.Inventory.Abstractions
{
    public interface IInventory
    {

    }

    /// <summary>
    /// Объект можно поместить в инвентарь.
    /// </summary>
    public interface IInventorizable
    {
        [Obsolete("Еще не реализовано")]
        InventoryTransferData GetInventoryData();
    }

    public interface IDeinventorizable
    {
        [Obsolete("Еще не реализовано")]
        void SetInventoryData(InventoryTransferData data);
    }
}