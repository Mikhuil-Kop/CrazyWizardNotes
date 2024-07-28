using System;
using UNet.Animations.Abstractions;
using UnityEngine;

namespace UNet.Animations.Realization
{
    public abstract class AnimationHandler : MonoBehaviour, IAnimationHandler
    {
        public event Action<IAnimationContext> OnAnimationStart;
        public event Action<IAnimationContext> OnAnimationUpdate;
        public event Action<IAnimationContext> OnAnimationEnd;

        public void RaiseOnAnimationStart(IAnimationContext context)
        {
            OnAnimationStart?.Invoke(context);
        }

        public void RaiseOnAnimationUpdate(IAnimationContext context)
        {
            OnAnimationUpdate?.Invoke(context);
        }

        public void RaiseOnAnimationEnd(IAnimationContext context)
        {
            OnAnimationEnd?.Invoke(context);
        }
    }
}