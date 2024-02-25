using System;
using CrazyWizardNotes.Model;
using CrazyWizardNotes.Spells.Logic;

namespace CrazyWizardNotes.Environment.Components.Interfaces
{
    public interface IJointComponent : IObjectComponent
    {
        void Connect(IPhysical target1, IPhysical target2);
        event OnJointBreakDelegate OnJointBreak;
    }
}
