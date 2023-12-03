using System.Collections.Generic;

namespace CrazyWizardNotes.UI.Abstractions
{
    /// <summary>
    /// Сервис, отвечающий за панель заклинаний, доступных игроку для применения.
    /// </summary>
    public interface IActionBarService : IEnumerable<KeyValuePair<string, IActionBarServiceItem>>
    {
        /// <summary>
        /// Кол-во заклинаний на панели.
        /// </summary>
        public byte Count { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionName">Название заклинания в списке.</param>
        /// <returns>Информация об заклинании в списке заклинаний. Null, если заклинание не найдено.</returns>
        IActionBarServiceItem this[string actionName] { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionName">Название заклинания.</param>
        /// <returns>Информация об заклинании в списке заклинаний. Null, если заклинание не найдено.</returns>
        IActionBarServiceItem AddAction(string actionName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionName">Название заклинания.</param>
        bool RemoveAction(string actionName);
    }
}
