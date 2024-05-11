using System;
using CrazyWizardNotes.Model.Events;

namespace CrazyWizardNotes.Interfaces
{
    public interface ITouches
    {
        event Action<OnTouchEventArgs> OnTouch;
    }
}
