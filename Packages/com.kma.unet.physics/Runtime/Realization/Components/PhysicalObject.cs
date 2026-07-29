using System;
using System.Collections.Generic;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;
using UnityEngine;

namespace UNet.Physics.Realization
{
    public class PhysicalObject : IPhysical, IHasSpaceObject, ITouches, IMovable, IHandlesStart, IHandlesUpdate
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

        public SpaceObject SpaceObject { get => GetSpaceObject(); set => SetSpaceObject(value); }
        public SpacePoint SpacePoint { get => GetSpacePoint(); set => SetSpacePoint(value); }

        IEnumerable<IActor> ITouches.TouchingActors => TouchingActors;

        public event Action<OnTouchEventArgs> OnTouch;

        public void ApplyForce(Vector2D force) => Rigidbody.AddForce(force, ForceMode2D.Force);
        public void ApplyImpulse(Vector2D force) => Rigidbody.AddForce(force, ForceMode2D.Impulse);
        public void Move(Vector2D delta) => Rigidbody.MovePosition(Transform.position + (Vector3)delta);

        private SpacePoint GetSpacePoint()
        {
            ParentSpace ??= PhysicsService.GetDefaultSpacePoint(Vector2D.Zero).ParentSpace;

            return new SpacePoint()
            {
                ParentSpace = ParentSpace,
                Position = Transform.position,
            };
        }

        private SpaceObject GetSpaceObject()
        {
            ParentSpace ??= PhysicsService.GetDefaultSpacePoint(Vector2D.Zero).ParentSpace;

            return new SpaceObject()
            {
                ParentSpace = ParentSpace,

                Position = Transform.position,
                Size = Transform.localScale,
                Rotation = Transform.eulerAngles.z
            };
        }

        private void SetSpacePoint(SpacePoint point)
        {
            ParentSpace = point.ParentSpace;
            Rigidbody.position = point.Position;
        }

        private void SetSpaceObject(SpaceObject obj)
        {
            ParentSpace = obj.ParentSpace;

            Transform.position = obj.Position;
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

        [Obsolete("Постоянное выполнение. Нужно поправить.")]
        public void OnUpdate(float deltaTime)
        {
            Transform.position = new Vector3(Transform.position.x, Transform.position.y, 0.1f * Transform.position.y);
        }

        #endregion

        #region Внутренние классы

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