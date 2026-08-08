namespace CrazyWizardNotes.Totems.Abstractions
{
    /// <summary>
    /// Тотем, создающий частицы заклинания.
    /// </summary>
    public interface ISpellParticleTotem : ITotem
    {
        string SpellId { get; }
    }
}
