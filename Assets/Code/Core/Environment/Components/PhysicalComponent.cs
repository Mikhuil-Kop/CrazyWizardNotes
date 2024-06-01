using System;
using System.Collections.Generic;
using CrazyWizardNotes.Interfaces;
using CrazyWizardNotes.Model.Events;
using UnityEngine;

namespace CrazyWizardNotes.Environment.Components
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PhysicalComponent : ObjectComponentBase, IPhysical, ISpaceObject, ITouches
    {
        private Rigidbody2D Rigidbody { get; set; }
        private Collider2D Collider { get; set; }

        private HashSet<ISubject> TouchingSubjects { get; set; }

        private event Action<OnTouchEventArgs> OnTouch;


        protected void Awake()
        {
            Rigidbody = Locate<Rigidbody2D>();
            Collider = Locate<Collider2D>();
            TouchingSubjects = new HashSet<ISubject>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            var subject = col.gameObject.GetComponent<ISubject>();

            var args = new OnTouchEventArgs()
            {
                Subject = subject
            };

            TouchingSubjects.Add(subject);
            OnTouch?.Invoke(args);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            var subject = col.gameObject.GetComponent<ISubject>();

            var args = new OnTouchEventArgs()
            {
                Subject = subject
            };

            TouchingSubjects.Add(subject);
            OnTouch?.Invoke(args);
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            var subject = col.gameObject.GetComponent<ISubject>();

            TouchingSubjects.Remove(subject);
        }

        private void OnCollisionExit2D(Collision2D col)
        {
            var subject = col.gameObject.GetComponent<ISubject>();

            TouchingSubjects.Remove(subject);
        }

        #region Интерфейс

        Vector2 ISpacePoint.Position => transform.position;
        Vector2 ISpaceObject.Size => transform.localScale;
        float ISpaceObject.Rotation => transform.rotation.z;

        float IPhysical.Mass => Rigidbody.mass;
        Vector2 IPhysical.ForceSummary => Rigidbody.totalForce;
        Vector2 IPhysical.SpeedSummary => Rigidbody.velocity;

        IEnumerable<ISubject> ITouches.TouchingSubjects => TouchingSubjects;
        event Action<OnTouchEventArgs> ITouches.OnTouch
        {
            add => OnTouch += value;
            remove => OnTouch -= value;
        }


        void IPhysical.ApplyForce(Vector2 force) => Rigidbody.AddForce(force, ForceMode2D.Force);
        void IPhysical.ApplyImpulse(Vector2 force) => Rigidbody.AddForce(force, ForceMode2D.Impulse);

        #endregion
    }
}