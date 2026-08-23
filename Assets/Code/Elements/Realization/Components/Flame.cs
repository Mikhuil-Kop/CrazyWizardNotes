using CrazyWizardNotes.Elements.Abstractions;
using UNet.Physics.Abstractions;
using UnityEngine;

namespace CrazyWizardNotes.Elements.Realization
{
    public class Flame : IFlame
    {
        private GameObject GameObject { get; }
        public bool IsActive { get; private set; }

        public Flame(GameObject gameObject)
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
