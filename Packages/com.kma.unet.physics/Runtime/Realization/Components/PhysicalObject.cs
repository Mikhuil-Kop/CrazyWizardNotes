using System;
using System.Collections.Generic;
using System.Linq;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;
using UnityEngine;

namespace UNet.Physics.Realization
{
    public class PhysicalObject : IPhysical, IHasSpaceObject, ITouches, IMovable, IPinable, IHasAdjustableMass, IHandlesStart, IHandlesUpdate, IHandlesFixedUpdate
    {
        #region Поля

        private IPhysicsService PhysicsService { get; }
        private IActor Actor { get; }
        private Transform Transform { get; }
        private Rigidbody2D Rigidbody { get; }
        private Joint2D OptionalJoint { get; set; }
        private LinkedList<IActor> TouchingActors { get; set; }
        private bool IsPined { get; set; }
        private float RemainingPinForce { get; set; }
        private const float PinFrictionDelta = 2000f;

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

        public float PhysicalMass => Rigidbody.mass;
        public float VolumeMass => Rigidbody.mass;
        public Vector2D ForceSummary => Rigidbody.totalForce;
        public Vector2D SpeedSummary => Rigidbody.linearVelocity;

        public SpaceObject SpaceObject { get => GetSpaceObject(); set => SetSpaceObject(value); }
        public SpacePoint SpacePoint { get => GetSpacePoint(); set => SetSpacePoint(value); }

        IEnumerable<IActor> ITouches.TouchingActors => TouchingActors;

        public event Action<OnTouchEventArgs> OnTouch;
        public event Action OnPin;
        public event Action OnUnpin;

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

        [Obsolete("Определиться с группировкой")]
        private string GetColliderName(Collider2D collider)
        {
            return null;
        }

        public void Pin(float forceToUnpin)
        {
            if (IsPined)
            {
                return;
            }

            IsPined = true;
            RemainingPinForce = forceToUnpin;

            Rigidbody.linearDamping += PinFrictionDelta;

            OnPin?.Invoke();
        }

        public void Unpin()
        {
            if (!IsPined)
            {
                return;
            }

            IsPined = false;
            RemainingPinForce = -1f;

            Rigidbody.linearDamping -= PinFrictionDelta;

            OnUnpin?.Invoke();
        }

        #endregion

        #region События Unity

        public void OnStart()
        {
            OptionalJoint = Transform.gameObject.GetComponent<Joint2D>();

            if (OptionalJoint != null)
            {
                OptionalJoint.breakAction = JointBreakAction2D.Disable;
            }

            var colliders = Transform
                .GetComponentsInChildren<Collider2D>(true)
                .Where(x => x != null);

            foreach (var collider in colliders)
            {
                collider.gameObject.AddComponent<CollisionTriggerProxy>().Initialize(this);
            }
        }

        protected virtual void OnCollisionEnter2D(GameObject target, Collision2D c, IColliderData colliderData)
        {
            var actors = c.gameObject.GetComponents<IActor>();

            foreach (var actor in actors)
            {
                TouchingActors.AddFirst(actor);
            }

            OnTouch?.Invoke(new OnTouchEventArgs()
            {
                InvokeActor = Actor,
                InvokeColliderName = GetColliderName(c.otherCollider),
                Touched = actors,
                Touches = TouchingActors,
            });
        }

        protected virtual void OnTriggerEnter2D(GameObject target, Collider2D c, IColliderData colliderData)
        {
            var actors = c.gameObject.GetComponents<IActor>();

            foreach (var actor in actors)
            {
                TouchingActors.AddFirst(actor);
            }

            OnTouch?.Invoke(new OnTouchEventArgs()
            {
                InvokeActor = Actor,
                InvokeColliderName = GetColliderName(c),
                Touched = actors,
                Touches = TouchingActors
            });
        }

        protected virtual void OnCollisionExit2D(GameObject target, Collision2D c, IColliderData colliderData)
        {
            var actors = c.gameObject.GetComponents<IActor>();

            foreach (var actor in actors)
            {
                TouchingActors.Remove(actor);
            }
        }

        protected virtual void OnTriggerExit2D(GameObject target, Collider2D c, IColliderData colliderData)
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
            Transform.position = new Vector3(Transform.position.x, Transform.position.y, 0.5f * Transform.position.y);
        }

        [Obsolete("Постоянное выполнение. Нужно поправить.")]
        public void OnFixedUpdate(float fixedDeltaTime)
        {
            if (IsPined)
            {
                //RemainingPinForce -= Rigidbody.totalForce.magnitude * fixedDeltaTime;

                if (RemainingPinForce <= 0f || (OptionalJoint != null && !OptionalJoint.enabled))
                {
                    Unpin();
                }
            }
        }

        #endregion

        #region Внутренние классы

        private class CollisionTriggerProxy : MonoBehaviour
        {
            private PhysicalObject Owner { get; set; }
            private IColliderData ColliderData { get; set; }

            public void Initialize(PhysicalObject owner)
            {
                Owner = owner;

                ColliderData = gameObject.GetComponent<IColliderData>() ?? new EmptyColliderData();
            }

            public void OnCollisionEnter2D(Collision2D c) => Owner.OnCollisionEnter2D(gameObject, c, ColliderData);
            public void OnTriggerEnter2D(Collider2D c) => Owner.OnTriggerEnter2D(gameObject, c, ColliderData);
            public void OnCollisionExit2D(Collision2D c) => Owner.OnCollisionExit2D(gameObject, c, ColliderData);
            public void OnTriggerExit2D(Collider2D c) => Owner.OnTriggerExit2D(gameObject, c, ColliderData);
        }

        private class EmptyColliderData : IColliderData
        {
            public T GetMetadata<T>() where T : class
            {
                return null;
            }
        }

        #endregion
    }
}
