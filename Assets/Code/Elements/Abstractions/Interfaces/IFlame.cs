using System;
using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Elements.Abstractions
{
    public interface IFlame
    {
        public bool IsActive { get; }
        public void Extinguish();
        public void SetPosition(SpaceObject ownerPosition);
    }
}
