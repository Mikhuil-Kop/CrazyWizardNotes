using UNet.Animations.Abstractions;
using UnityEngine;

namespace CrazyWizardNotes.Creatures.Realization
{
    public class CreatureAnimationDataMonoBehavior : MonoBehaviour, IHasAnimationData<CreatureAnimationData>
    {
        public bool IsEating;
        public bool IsBusy;

        private CreatureAnimationData Data = new();
        public CreatureAnimationData AnimationValue
        {
            get
            {
                Data.IsEating = IsEating;
                Data.IsBusy = IsBusy;

                return Data;
            }
        }
    }
}
