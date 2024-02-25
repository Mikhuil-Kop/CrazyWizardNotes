using System;

namespace CrazyWizardNotes.Environment.Components.Interfaces
{
    public interface IDestructionComponent : IObjectComponent
    {
        bool CanVanish { get; }
        bool CanDispell { get; }
        bool CanDestruct { get; }
        bool CanExplode { get; }

        void Vanish();
        void Dispell();
        void Destruct();
        void Explode();

        event Action OnDispell;
        event Action OnVanish;
        event Action OnExplode;
        event Action OnDestruct;
    }
}
