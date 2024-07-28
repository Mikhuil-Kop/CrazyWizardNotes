using System;
using System.Collections.Generic;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;
using UnityEngine;

namespace UNet.Physics.Realisation
{
    public class PhysicalObject : IPhysical, ISpacePoint, ISpaceObject, ITouches, IMovable
    {
        #region Поля

        private Transform Transform { get; }
        private Rigidbody2D Rigidbody { get; }

        public LinkedList<IActor> TouchingSubjects { get; set; }

        public PhysicalObject(Transform transform, Rigidbody2D physicsBody2D)
        {
            Transform = transform;
            Rigidbody = physicsBody2D;

            TouchingSubjects = new LinkedList<IActor>();
        }

        #endregion

        #region Реализация компонентов

        public ISpaceContainer ParentSpace { get; set; }

        public Vector2D Position => Transform.position;
        public Vector2D Size => Transform.localScale;
        public float Rotation => Transform.rotation.z;

        public float Mass => Rigidbody.mass;
        public Vector2D ForceSummary => Rigidbody.totalForce;
        public Vector2D SpeedSummary => Rigidbody.linearVelocity;

        IEnumerable<IActor> ITouches.TouchingSubjects => TouchingSubjects;
        public event Action<OnTouchEventArgs> OnTouch;

        public void ApplyForce(Vector2D force) => Rigidbody.AddForce(force, ForceMode2D.Force);
        public void ApplyImpulse(Vector2D force) => Rigidbody.AddForce(force, ForceMode2D.Impulse);
        public void Move(Vector2D delta) => Rigidbody.MovePosition(Position + delta);

        #endregion

        #region События Unity

        protected virtual void OnCollisionEnter(Collision c)
        {
            var actors = c.gameObject.GetComponents<IActor>();

            foreach (var actor in actors)
            {
                TouchingSubjects.AddFirst(actor);
            }

            foreach (var actor in actors)
            {
                OnTouch?.Invoke(new OnTouchEventArgs()
                {
                    Subject = actor
                });
            }
        }

        protected virtual void OnTriggerEnter(Collider c)
        {
            var actors = c.gameObject.GetComponents<IActor>();

            foreach (var actor in actors)
            {
                TouchingSubjects.AddFirst(actor);
            }

            foreach (var actor in actors)
            {
                OnTouch?.Invoke(new OnTouchEventArgs()
                {
                    Subject = actor
                });
            }
        }

        protected virtual void OnCollisionExit(Collision c)
        {
            var actors = c.gameObject.GetComponents<IActor>();

            foreach (var actor in actors)
            {
                TouchingSubjects.Remove(actor);
            }
        }

        protected virtual void OnTriggerExit(Collider c)
        {
            var actors = c.gameObject.GetComponents<IActor>();

            foreach (var actor in actors)
            {
                TouchingSubjects.Remove(actor);
            }
        }

        #endregion
    }
}