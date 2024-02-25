using System;
using UnityEngine;
using CrazyWizardNotes.Spells.Logic;
using CrazyWizardNotes.Model.EventArguments;

namespace CrazyWizardNotes.Spells.SpellParticles
{
    public class DefaultSpellParticle : MonoBehaviour, ISpellParticle, IPhysical, ISpaceObject, IVanishable, ITouches, IDispellable, IExplosive
    {
        #region Поля

        [SerializeField]
        private Rigidbody2D Rigidbody;

        [SerializeField]
        private Collider2D Collider;

        event Action IVanishable.OnVanish
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        event Action IDispellable.OnDispell
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        event Action IExplosive.OnExplode
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region Поведение

        private void OnTriggerEnter2D(Collider2D col)
        {
            OnTouch?.Invoke(new OnTouchEventArgs());
        }

        #endregion

        #region Интерфейсы

        Vector2 ISpaceObject.Size => transform.localScale;

        float ISpaceObject.Rotation => transform.rotation.z;

        Vector2 ISpacePoint.Position => transform.position;

        bool IVanishable.CanVanish => throw new NotImplementedException();

        bool IDispellable.CanDispell => throw new NotImplementedException();

        bool IExplosive.CanExplode => throw new NotImplementedException();

        public event Action<OnTouchEventArgs> OnTouch;

        void IPhysical.ApplyForce(Vector2 force) => Rigidbody.AddForce(force, ForceMode2D.Force);

        void IPhysical.ApplyImpulse(Vector2 force) => Rigidbody.AddForce(force, ForceMode2D.Impulse);

        void IVanishable.Vanish()
        {
            if (this != null && gameObject != null)
            {
                Destroy(gameObject);
            }
        }

        void IDispellable.Dispell()
        {
            if (this != null && gameObject != null)
            {
                Destroy(gameObject);
            }
        }

        void IExplosive.Explode()
        {
            if (this != null && gameObject != null)
            {
                Destroy(gameObject);
            }
        }

        #endregion

    }
}