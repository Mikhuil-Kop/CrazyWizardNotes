using System.Collections.Generic;

namespace CrazyWizardNotes.Magic.Abstractions
{
    /// <summary>
    /// Сервис, отвечающий за панель заклинаний, доступных игроку для применения.
    /// </summary>
    public interface ISpellBarService : IEnumerable<KeyValuePair<string, ISpellBarServiceItem>>
    {
        /// <summary>
        /// Кол-во заклинаний на панели.
        /// </summary>
        public byte Count { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spellName">Название заклинания в списке.</param>
        /// <returns>Информация об заклинании в списке заклинаний. Null, если заклинание не найдено.</returns>
        ISpellBarServiceItem this[string spellName] { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spellName">Название заклинания.</param>
        /// <returns>Информация об заклинании в списке заклинаний. Null, если заклинание не найдено.</returns>
        ISpellBarServiceItem AddSpell(string spellName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spellName">Название заклинания.</param>
        void RemoveSpell(string spellName);
    }
}
