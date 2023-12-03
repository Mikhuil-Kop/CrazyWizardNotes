using System;
using UNet.Core.Abstractions;
using UnityEngine;

namespace UNet.Core.Realization
{
    public class Destroyable : IDestroyable
    {
        public event Action OnDestroy;

        public bool IsDestroyed { get; private set; }
        
        private GameObject GameObject { get; }

        public Destroyable(GameObject gameObject)
        {
            GameObject = gameObject;
        }

        public void Destroy()
        {
            if (IsDestroyed)
            {
                return;
            }

            IsDestroyed = true;

            OnDestroy?.Invoke();

            GameObject.Destroy(GameObject);
        }
    }
}
