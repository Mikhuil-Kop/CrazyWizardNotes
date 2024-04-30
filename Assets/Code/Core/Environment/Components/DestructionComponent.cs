using System;
using CrazyWizardNotes.Interfaces;
using UnityEngine;

namespace CrazyWizardNotes.Environment.Components
{
    public class DestructionComponent : ObjectComponentBase, IVanishable, IDispellable, IDestructible, IStopsExisting
    {
        [SerializeField]
        private bool CanVanish;
        [SerializeField]
        private bool CanDispell;
        [SerializeField]
        private bool CanDestruct;
        [SerializeField]
        private float DestructionForce;

        private IPhysical Physical { get; set; }

        protected override void Start()
        {
            base.Start();

            Physical = Parent.As<IPhysical>();
        }

        protected void FixedUpdate()
        {
            if (DestructionForce > 0 && DestructionForce < Physical?.ForceSummary.magnitude)
            {
                Destruct();
            }
        }

        #region Интерфейс

        bool IVanishable.CanVanish => CanVanish;
        bool IDispellable.CanDispell => CanDispell;
        bool IDestructible.CanDestruct => CanDestruct;

        public event Action OnDispell;
        public event Action OnVanish;
        public event Action OnDestruct;
        public event Action OnStopExisting;

        public void Vanish()
        {
            if (!CanVanish)
            {
                return;
            }

            OnVanish?.Invoke();
            Destroy();
        }

        public void Dispell()
        {
            if (!CanDispell)
            {
                return;
            }

            OnDispell?.Invoke();
            Destroy();
        }

        public void Destruct()
        {
            if (!CanDestruct)
            {
                return;
            }

            OnDestruct?.Invoke();
            Destroy();
        }

        public void StopExisting()
        {
            Destroy();
        }

        private void Destroy()
        {
            OnStopExisting?.Invoke();
            Destroy(gameObject);
        }

        #endregion
    }
}