using UnityEngine;

namespace CrazyWizardNotes.Creatures.Realization
{
    public class HumanVisualizer : MonoBehaviour, ICreatureVisualizer
    {
        public Animator Animator;
        public HumanSkin Skin;

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
