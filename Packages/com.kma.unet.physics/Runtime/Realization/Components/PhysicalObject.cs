using System;
using System.Collections.Generic;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;
using UnityEngine;

namespace UNet.Physics.Realization
{
    public class PhysicalObject : IPhysical, IHasSpaceObject, ITouches, IMovable, IHandlesStart
    {
        #region Поля

        private IPhysicsService PhysicsService { get; }
        private IActor Actor { get; }
        private Transform Transform { get; }
        private Rigidbody2D Rigidbody { get; }
        private LinkedList<IActor> TouchingActors { get; set; }

        public PhysicalObject(IPhysicsService physicsService, IActor actor, Transform transform, Rigidbody2D physicsBody2D)
        {
            PhysicsService = physicsService;
            Actor = actor;
            Transform = transform;
            Rigidbody = physicsBody2D;

            TouchingActors = new LinkedList<IActor>();
        }

        #endregion

        #region Реализация компонентов

        public ISpaceContainer ParentSpace { get; set; }

        public float Mass => Rigidbody.mass;
        public Vector2D ForceSummary => Rigidbody.totalForce;
        public Vector2D SpeedSummary => Rigidbody.linearVelocity;

        public ISpaceObject SpaceObject { get => GetSpace(); set => SetSpace(value); }
        public ISpacePoint SpacePoint { get => GetSpace(); set => SetSpace(value); }

        IEnumerable<IActor> ITouches.TouchingActors => TouchingActors;

        public event Action<OnTouchEventArgs> OnTouch;

        public void ApplyForce(Vector2D force) => Rigidbody.AddForce(force, ForceMode2D.Force);
        public void ApplyImpulse(Vector2D force) => Rigidbody.AddForce(force, ForceMode2D.Impulse);
        public void Move(Vector2D delta) => Rigidbody.MovePosition(Transform.position + (Vector3)delta);

        private ISpaceObject GetSpace()
        {
            ParentSpace ??= PhysicsService.GetDefaultSpacePoint(Vector2D.Zero).ParentSpace;

            return new VectorSpaceObject()
            {
                ParentSpace = ParentSpace,
                Position = Transform.position,
                Size = Transform.localScale,
                Rotation = Transform.eulerAngles.z
            };
        }

        private void SetSpace(ISpacePoint point)
        {
            ParentSpace = point.ParentSpace;
            Rigidbody.position = point.Position;

            if (point is not ISpaceObject)
            {
                return;
            }

            var obj = point as ISpaceObject;

            Transform.localScale = new Vector3(obj.Size.X, obj.Size.Y, 1);
            Transform.eulerAngles = new Vector3(0, 0, obj.Rotation);
        }

        #endregion

        #region События Unity

        public void OnStart()
        {
            Transform.gameObject.AddComponent<CollisionTriggerProxy>().Initialize(this);
        }

        protected virtual void OnCollisionEnter2D(Collision2D c)
        {
            var actors = c.gameObject.GetComponents<IActor>();

            foreach (var actor in actors)
            {
                TouchingActors.AddFirst(actor);
            }

            OnTouch?.Invoke(new OnTouchEventArgs()
            {
                InvokeActor = Actor,
                Touched = actors,
                Touches = TouchingActors,
            });
        }

        protected virtual void OnTriggerEnter2D(Collider2D c)
        {
            var actors = c.gameObject.GetComponents<IActor>();

            foreach (var actor in actors)
            {
                TouchingActors.AddFirst(actor);
            }

            OnTouch?.Invoke(new OnTouchEventArgs()
            {
                InvokeActor = Actor,
                Touched = actors,
                Touches = TouchingActors
            });
        }

        protected virtual void OnCollisionExit2D(Collision2D c)
        {
            var actors = c.gameObject.GetComponents<IActor>();

            foreach (var actor in actors)
            {
                TouchingActors.Remove(actor);
            }
        }

        protected virtual void OnTriggerExit2D(Collider2D c)
        {
            var actors = c.gameObject.GetComponents<IActor>();

            foreach (var actor in actors)
            {
                TouchingActors.Remove(actor);
            }
        }

        #endregion

        #region Внутренние классы

        private class VectorSpaceObject : ISpaceObject
        {
            public ISpaceContainer ParentSpace { get; set; }
            public Vector2D Position { get; set; }
            public Vector2D Size { get; set; }
            public float Rotation { get; set; }
        }

        public class CollisionTriggerProxy : MonoBehaviour
        {
            private PhysicalObject Owner { get; set; }
            public void Initialize(PhysicalObject owner)
            {
                Owner = owner;
            }

            public void OnCollisionEnter2D(Collision2D c) => Owner.OnCollisionEnter2D(c);
            public void OnTriggerEnter2D(Collider2D c) => Owner.OnTriggerEnter2D(c);
            public void OnCollisionExit2D(Collision2D c) => Owner.OnCollisionExit2D(c);
            public void OnTriggerExit2D(Collider2D c) => Owner.OnTriggerExit2D(c);
        }

        #endregion
    }
}