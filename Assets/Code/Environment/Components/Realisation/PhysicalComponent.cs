using System;
using CrazyWizardNotes.Environment.Components.Interfaces;
using CrazyWizardNotes.Model.EventArguments;
using CrazyWizardNotes.Spells.Logic;
using UnityEngine;

namespace CrazyWizardNotes.Environment.Components.Realisation
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PhysicalComponent : ObjectComponentBase, IPhysicalComponent, ISpaceObjectComponent, ITouches
    {
        private Rigidbody2D Rigidbody { get; set; }
        private Collider2D Collider2D { get; set; }

        public event Action<OnTouchEventArgs> OnTouch;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            OnTouch?.Invoke(new OnTouchEventArgs());
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            OnTouch?.Invoke(new OnTouchEventArgs());
        }

        #region Интерфейс

        Vector2 ISpaceObjectComponent.Size => transform.localScale;

        float ISpaceObjectComponent.Rotation => transform.rotation.z;

        Vector2 ISpaceObjectComponent.Position => transform.position;

        float IPhysicalComponent.Mass => Rigidbody.mass;

        void IPhysicalComponent.ApplyForce(Vector2 force)
        {
            Rigidbody.AddForce(force, ForceMode2D.Force);
        }

        void IPhysicalComponent.ApplyImpulse(Vector2 force)
        {
            Rigidbody.AddForce(force, ForceMode2D.Impulse);
        }

        #endregion
    }
}