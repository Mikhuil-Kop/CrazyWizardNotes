using System;
using CrazyWizardNotes.Model.EventArguments;
using CrazyWizardNotes.Interfaces;
using UnityEngine;
using System.Collections.Generic;

namespace CrazyWizardNotes.Environment.Components
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PhysicalComponent : ObjectComponentBase, IPhysical, ISpaceObject, ITouches
    {
        private Rigidbody2D Rigidbody { get; set; }
        private Collider2D Collider2D { get; set; }

        public event Action<OnTouchEventArgs> OnTouch;

        protected void Awake()
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

        Vector2 ISpacePoint.Position => transform.position;
        Vector2 ISpaceObject.Size => transform.localScale;
        float ISpaceObject.Rotation => transform.rotation.z;

        float IPhysical.Mass => Rigidbody.mass;
        Vector2 IPhysical.ForceSummary => Rigidbody.totalForce;
        Vector2 IPhysical.SpeedSummary => Rigidbody.velocity;
        void IPhysical.ApplyForce(Vector2 force) => Rigidbody.AddForce(force, ForceMode2D.Force);
        void IPhysical.ApplyImpulse(Vector2 force) => Rigidbody.AddForce(force, ForceMode2D.Impulse);

        #endregion
    }
}