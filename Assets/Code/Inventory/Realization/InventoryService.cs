using CrazyWizardNotes.Inventory.Abstractions;
using UNet.Common;
using UNet.Core.Abstractions;
using UNet.Inputs.Abstractions;
using UnityEngine;

namespace CrazyWizardNotes.Inventory.Realization
{
    public class InventoryService : IInventoryService
    {
        public IPointerInputService PointerInputService { get; }
        public RectTransform RectTransform { get; set; }
        public IInventory CurrentInventory { get; private set; }

        public void Show(IInventory inventory)
        {
            Exceptions.CheckArgumentNull(inventory, nameof(inventory));

            CurrentInventory = inventory;
        }

        public void Hide(IInventory inventory)
        {
            Exceptions.CheckArgumentNull(inventory, nameof(inventory));

            if (CurrentInventory == inventory)
            {
                CurrentInventory = null;
            }
        }

        public IActor Push(IInventory inventory, IInventorizable inventorizable)
        {
            Exceptions.CheckArgumentNull(inventory, nameof(inventory));
            Exceptions.CheckArgumentNull(inventorizable, nameof(inventorizable));

            throw new System.NotImplementedException();
        }

        public IActor Pull(IInventory inventory, IDeinventorizable inventorizable)
        {
            Exceptions.CheckArgumentNull(inventory, nameof(inventory));
            Exceptions.CheckArgumentNull(inventorizable, nameof(inventorizable));
            
            throw new System.NotImplementedException();
        }

        public IActor Load(IInventory inventory, string key)
        {
            Exceptions.CheckArgumentNull(inventory, nameof(inventory));
            Exceptions.CheckArgumentNull(key, nameof(key));
            
            throw new System.NotImplementedException();
        }

        public void Save(IInventory inventory, IInventoryItem item, string key)
        {
            Exceptions.CheckArgumentNull(inventory, nameof(inventory));
            Exceptions.CheckArgumentNull(item, nameof(item));
            Exceptions.CheckArgumentNull(key, nameof(key));
        }

        public void Remove(IInventory inventory, IInventoryItem item)
        {
            Exceptions.CheckArgumentNull(inventory, nameof(inventory));
            Exceptions.CheckArgumentNull(item, nameof(item));
        }
    }
}