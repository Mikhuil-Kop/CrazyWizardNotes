namespace CrazyWizardNotes.Magic
{
    /// <summary>
    /// Управляется магическим потоком.
    /// </summary>
    public interface ISpellBendable
    {
        public void OnStartBend(SpellBendStartEventArgs args);
        public void OnUpdateBend(SpellBendUpdateEventArgs args);
        public void OnFixedUpdateBend(SpellBendUpdateEventArgs args);
        public void OnEndBend(SpellBendEndEventArgs args);
    }
}