using UNet.Physics.Abstractions;
using UnityEngine;

namespace UNet.Physics.Realization
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class ColliderDataBase<T> : MonoBehaviour, IColliderData where T : class
    {
        [SerializeField]
        public T Metadata;

        public T1 GetMetadata<T1>() where T1 : class
        {
            return Metadata as T1;
        }
    }
}