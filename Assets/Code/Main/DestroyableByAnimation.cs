using System;
using UNet.Animations.Abstractions;
using UNet.Core.Abstractions;
using UnityEngine;

namespace CrazyWizardNotes.Main
{
    public class DestroyableByAnimation : IDestroyable
    {
        private GameObject GameObject { get; }
        private IHasAnimationEvents HasAnimationEvents { get; }

        private bool IsDestroyRequested { get; set; }
        private bool IsDestroyAnimated { get; set; }
        private bool IsDestroySuccessful { get; set; }
        public bool IsDestroyed => IsDestroyRequested;
        public event Action OnDestroy;

        public DestroyableByAnimation(GameObject gameObject, IHasAnimationEvents hasAnimationEvents)
        {
            GameObject = gameObject;
            HasAnimationEvents = hasAnimationEvents;

            HasAnimationEvents.OnAnimationEvent += HandleAnimationEvent;
        }

        public void Destroy()
        {
            IsDestroyRequested = true;

            TryDestroy();
        }

        private void HandleAnimationEvent(string obj)
        {
            if (obj == "Disappeared")
            {
                IsDestroyAnimated = true;

                TryDestroy();
            }
        }

        private void TryDestroy()
        {
            if (IsDestroySuccessful)
            {
                return;
            }

            if (!IsDestroyRequested || !IsDestroyAnimated)
            {
                return;
            }

            IsDestroySuccessful = true;

            OnDestroy?.Invoke();

            GameObject.Destroy(GameObject);
        }
    }
}
