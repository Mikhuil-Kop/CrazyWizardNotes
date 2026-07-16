namespace CrazyWizardNotes.Magic.Abstractions
{
    /// <summary>
    /// Информация об заклинании, указанном на панели.
    /// </summary>
    public interface ISpellBarServiceItem
    {
        public string SpellName { get; }
        public string SpellTitle { get; }
        public string SpellDescription { get; }
        public string SpellHotkey { get; }
        public bool IsEnabled { get; set; }
    }
}
