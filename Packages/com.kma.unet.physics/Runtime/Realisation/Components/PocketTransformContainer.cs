using UNet.Physics.Abstractions;
using UnityEngine;

namespace UNet.Physics.Realisation
{
    public class PocketTransformContainer : ISpaceContainer
    {
        public Transform Transform { get; }
        public ISpaceContainer ParentSpace { get; set; }

        public PocketTransformContainer(Transform transform)
        {
            Transform = transform;
        }

        public Vector2D TranslateToParentSpace(Vector2D vector)
        {
            return Transform.position;
        }
    }
}