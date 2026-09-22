using UnityEngine;

namespace UNet.Animations.Realization
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorExt : MonoBehaviour
    {
        public void RaiseAnimationEvent(string eventName)
        {

        }


        private enum AnimationCommonEvents
        {
            Disappeared = 1
        }
    }
}