using UnityEngine;

namespace CrazyWizardNotes.Creatures.Realization
{
    public class CreatureVisualizer : MonoBehaviour, ICreatureVisualizer
    {
        public Animator Animator;

        public void StartEatAnimation()
        {
            Animator.SetTrigger("StartEat");
        }

        public void StartInteractAnimation()
        {
            Animator.SetTrigger("StartInteract");
        }
    }
}
