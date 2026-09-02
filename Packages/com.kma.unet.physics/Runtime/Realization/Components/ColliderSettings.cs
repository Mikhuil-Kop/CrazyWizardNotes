using System;
using UnityEngine;

namespace UNet.Physics.Realization
{
    [RequireComponent(typeof(Collider2D))]
    [Obsolete("Пока не используется. Система для группировки типов коллайдеров.")]
    public class ColliderSettings : MonoBehaviour
    {
        public Collider2D Collider;
        public string ColliderCode;
    }
}