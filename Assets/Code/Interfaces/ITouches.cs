using System;
using CrazyWizardNotes.Model.EventArguments;

namespace CrazyWizardNotes.Interfaces
{
    public interface ITouches
    {
        event Action<OnTouchEventArgs> OnTouch;
    }
}
