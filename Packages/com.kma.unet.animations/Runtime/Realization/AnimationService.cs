using System;
using System.Linq;
using UNet.Animations.Abstractions;
using UnityEngine;

namespace UNet.Animations.Realization
{
    public class AnimationService : IAnimationService
    {
        private Animator Animator { get; }

        public event Action<IAnimationContext> OnAnimationStateChange;

        public AnimationService(Animator animator)
        {
            Animator = animator;
        }

        public void Trigger(string key)
        {
            var triggers = Animator.parameters.Where(x => x.type == AnimatorControllerParameterType.Trigger);

            foreach (var trigger in triggers)
            {
                Animator.ResetTrigger(trigger.name);
            }

            Animator.SetTrigger(key);
        }

        public void SetParam(string key, int value)
        {
            Animator.SetInteger(key, value);
        }

        public void SetParam(string key, float value)
        {
            Animator.SetFloat(key, value);
        }

        public void SetParam(string key, bool value)
        {
            Animator.SetBool(key, value);
        }
    }
}