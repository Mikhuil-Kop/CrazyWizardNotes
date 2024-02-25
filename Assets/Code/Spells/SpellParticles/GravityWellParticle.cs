using System;
using UnityEngine;
using CrazyWizardNotes.Spells.Logic;
using UnityEngine.U2D;

namespace CrazyWizardNotes.Spells.SpellParticles
{
    public class GravityWellParticle : MonoBehaviour, ISpellParticle, ISpaceObject, IVanishable, IDispellable
    {
        #region Поля

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

        #endregion

        #region Поведение

        private void OnTriggerStay2D(Collider2D col)
        {
            var direction = (Vector2)(transform.rotation * Vector2.up);
            var rigidbody = col.attachedRigidbody;

            var force = 3f * direction;

            rigidbody.AddForce(force);
        }

        #endregion

        #region Интерфейсы

        Vector2 ISpaceObject.Size => transform.localScale;

        float ISpaceObject.Rotation => transform.rotation.z;

        Vector2 ISpacePoint.Position => transform.position;

        bool IVanishable.CanVanish => throw new NotImplementedException();

        bool IDispellable.CanDispell => throw new NotImplementedException();

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

        #endregion

    }
}