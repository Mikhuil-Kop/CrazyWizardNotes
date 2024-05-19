using UnityEngine;

namespace CrazyWizardNotes.Magic
{
    public interface ISpellsService
    {
        void EndBending(ISpellBendable spell, Vector2 target);
        void StartBending(ISpellBendable spell, Vector2 target);
        void UpdateBending(ISpellBendable spell, Vector2 target);
    }
}