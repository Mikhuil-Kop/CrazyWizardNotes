using System;
using UNet.Animations.Abstractions;
using UnityEngine;

namespace UNet.Animations.Realization
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorExt : MonoBehaviour, IHasAnimationEvents
    {
        public event Action<string> OnAnimationEvent;

        public void RaiseAnimationEvent(string eventName)
        {
            OnAnimationEvent?.Invoke(eventName);
        }

        private enum AnimationCommonEvents
        {
            Disappeared = 1
        }
    }
}