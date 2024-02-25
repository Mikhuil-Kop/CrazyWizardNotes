using System;
using CrazyWizardNotes.Environment.Components.Interfaces;
using UnityEngine;

namespace CrazyWizardNotes.Environment.Components.Realisation
{
    public class DestructionComponent : ObjectComponentBase, IDestructionComponent
    {
        [SerializeField]
        private bool CanVanish;
        [SerializeField]
        private bool CanDispell;
        [SerializeField]
        private bool CanDestruct;
        [SerializeField]
        private bool CanExplode;

        #region Интерфейс

        bool IDestructionComponent.CanVanish => CanVanish;
        bool IDestructionComponent.CanDispell => CanDispell;
        bool IDestructionComponent.CanDestruct => CanDestruct;
        bool IDestructionComponent.CanExplode => CanExplode;

        public event Action OnDispell;
        public event Action OnVanish;
        public event Action OnExplode;
        public event Action OnDestruct;

        void IDestructionComponent.Vanish()
        {
            if (CanVanish)
            {
                return;
            }

            OnVanish?.Invoke();
        }

        void IDestructionComponent.Dispell()
        {
            if (CanDispell)
            {
                return;
            }

            OnDispell?.Invoke();
        }

        void IDestructionComponent.Destruct()
        {
            if (CanDestruct)
            {
                return;
            }

            OnDestruct?.Invoke();
        }

        void IDestructionComponent.Explode()
        {
            if (CanExplode)
            {
                return;
            }

            OnExplode?.Invoke();
        }


        #endregion
    }
}