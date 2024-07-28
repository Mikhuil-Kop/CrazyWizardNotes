namespace CrazyWizardNotes.Magic.Abstractions
{
    /// <summary>
    /// Объект управляется магическим потоком.
    /// </summary>
    public interface ISpellBendable
    {
        public void OnStartBend(SpellBendStartEventArgs args);
        public void OnUpdateBend(SpellBendUpdateEventArgs args);
        public void OnEndBend(SpellBendEndEventArgs args);
    }
}