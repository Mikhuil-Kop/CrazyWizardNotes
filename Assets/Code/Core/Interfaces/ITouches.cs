using System;
using System.Collections.Generic;
using CrazyWizardNotes.Model.Events;

namespace CrazyWizardNotes.Interfaces
{
    public interface ITouches
    {
        IEnumerable<ISubject> TouchingSubjects { get; }
        event Action<OnTouchEventArgs> OnTouch;
    }
}
