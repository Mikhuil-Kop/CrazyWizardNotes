using System;
using System.Collections.Generic;

namespace CrazyWizardNotes.Common.Services
{
    public interface IEventsService
    {
        void RaiseEvent(EventType eventType, EventParams eventParams);
        IEnumerable<EventParams> GetRaisedEvents(EventType eventType);
    }
}