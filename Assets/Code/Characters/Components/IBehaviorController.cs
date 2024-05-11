using System;

namespace CrazyWizardNotes.Characters
{
    public interface IBehaviorController
    {
        event Action<string> OnEventCall;

        void RaiseEvent(string eventName, string param = null);
    }
}