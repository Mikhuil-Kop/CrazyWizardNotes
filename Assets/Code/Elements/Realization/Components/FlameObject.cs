using CrazyWizardNotes.Elements.Abstractions;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;
using UnityEngine;

namespace CrazyWizardNotes.Elements.Realization
{
    public class FlameObject : IFlame, IHasParent
    {
        private GameObject GameObject { get; }
        public IActor Parent { get => null; set => throw new System.NotImplementedException(); }
        public bool IsActive { get; private set; }

        public FlameObject(GameObject gameObject)
        {
            GameObject = gameObject;
            IsActive = true;
        }

        public void Extinguish()
        {
            IsActive = false;
            GameObject.Destroy(GameObject);
        }

        public void SetPosition(SpaceObject ownerPosition)
        {
            GameObject.transform.position = ownerPosition.Position;
        }
    }
}
