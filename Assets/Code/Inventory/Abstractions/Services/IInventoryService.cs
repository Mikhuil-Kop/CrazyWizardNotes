using UNet.Core.Abstractions;

namespace CrazyWizardNotes.Inventory.Abstractions
{
    /// <summary>
    /// Класс, отвечающий за управление инвентарем персонажа. Инвентарь позволяет хранить объекты в виртуальном пространстве, управляемым игроком.
    /// </summary>
    public interface IInventoryService
    {
        /// <summary>
        /// Текущий открытый инвентарь. Пока инвентарь открыт, им возможно управлять вручную.
        /// </summary>
        IInventory CurrentInventory { get; }

        /// <summary>
        /// Отображает интерфейс инвентаря.
        /// </summary>
        void Show(IInventory inventory);

        /// <summary>
        /// Скрывает интерфейс инвентаря.
        /// </summary>
        void Hide(IInventory inventory);

        /// <summary>
        /// Извлекает объект из пространства и помещает его в инвентарь.
        /// </summary>
        /// <returns>Объект в инвентаре.</returns>
        IActor Push(IInventory inventory, IInventorizable inventorizable);
        
        /// <summary>
        /// Извлекает объект из инвентаря и помещает его в пространство.
        /// </summary>
        /// <returns>Объект в пространстве.</returns>
        IActor Pull(IInventory inventory, IDeinventorizable inventorizable);

        /// <summary>
        /// Загружает объект, возвращяя его в пространство инвентаря.
        /// </summary>
        /// <returns>Объект в инвентаре.</returns>
        IActor Load(IInventory inventory, string key);

        /// <summary>
        /// Сохраняет объект, позволяя впоследствии его загрузить.
        /// </summary>
        void Save(IInventory inventory, IInventoryItem item, string key);

        /// <summary>
        /// Убирает объект из пространства инвентаря.
        /// </summary>
        void Remove(IInventory inventory, IInventoryItem item);
    }
}