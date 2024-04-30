namespace CrazyWizardNotes.Spells.Logic
{
    public interface ISpell
    {
        void EndCust(float deltaTime);
        void TickCust(float deltaTime);
        void StartCast(float deltaTime);
    }
}