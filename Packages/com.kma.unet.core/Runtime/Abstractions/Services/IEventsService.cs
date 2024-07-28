using System.Collections.Generic;

namespace UNet.Core.Abstractions
{
    public interface IEventsService
    {
        void RaiseEvent(EventType eventType, EventParams eventParams);
        IEnumerable<EventParams> GetRaisedEvents(EventType eventType);
    }
}