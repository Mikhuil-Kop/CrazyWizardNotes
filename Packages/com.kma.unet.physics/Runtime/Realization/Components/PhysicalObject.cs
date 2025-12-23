using System;
using System.Collections.Generic;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;
using UnityEngine;

namespace UNet.Physics.Realization
{
    public class PhysicalObject : IPhysical, ISpacePoint, ISpaceObject, IEditableSpacePoint, ITouches, IMovable
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

        [Obsolete("Должно set-ится через функцию")]
        public Vector2D Position { get => Transform.position; set => Transform.position = value; }
        [Obsolete("Должно set-ится через функцию")]
        public Vector2D Size { get => Transform.localScale; set => Transform.localScale = value; }
        [Obsolete("Должно set-ится через функцию")]
        public float Rotation { get => Transform.rotation.z; set => Transform.rotation = Quaternion.Euler(0, 0, value); }

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