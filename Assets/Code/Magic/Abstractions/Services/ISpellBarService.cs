using System.Collections.Generic;

namespace CrazyWizardNotes.Magic.Abstractions
{
    /// <summary>
    /// Сервис, отвечающий за панель заклинаний, доступных игроку для применения.
    /// </summary>
    public interface ISpellBarService : IEnumerable<KeyValuePair<string, ISpellBarItem>>
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
        ISpellBarItem this[string spellName] { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spellName">Название заклинания.</param>
        /// <returns>Информация об заклинании в списке заклинаний. Null, если заклинание не найдено.</returns>
        ISpellBarItem AddSpell(string spellName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spellName">Название заклинания.</param>
        void RemoveSpell(string spellName);
    }

    /// <summary>
    /// Информация об заклинании, указанном на панели.
    /// </summary>
    public interface ISpellBarItem
    {
        public string SpellName { get; }
        public string SpellTitle { get; }
        public string SpellDescription { get; }
        public string SpellHotkey { get; }
        public bool IsEnabled { get; set; }
    }
}
