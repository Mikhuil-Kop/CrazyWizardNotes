using System;
using System.Collections.Generic;
using System.Linq;
using UNet.Core.Abstractions;

namespace UNet.Core.Realization
{
    public class EventsService : IEventsService, IHandlesLateUpdate
    {
        private Queue<EventParams>[] ProcessingEvents { get; set; }
        private Queue<EventParams>[] RaisedEvents { get; set; }

        public EventsService()
        {
            var length = Enum
                .GetValues(typeof(EventType))
                .Cast<EventType>()
                .Max(x => (int)x) + 1;

            ProcessingEvents = Enumerable
                .Range(0, length)
                .Select(x => new Queue<EventParams>())
                .ToArray();

            RaisedEvents = Enumerable
                .Range(0, length)
                .Select(x => new Queue<EventParams>())
                .ToArray();
        }

        public void OnLateUpdate(float deltaTime)
        {
            foreach (var events in ProcessingEvents)
            {
                events.Clear();
            }

            (RaisedEvents, ProcessingEvents) = (ProcessingEvents, RaisedEvents);
        }

        public void RaiseEvent(EventType eventType, EventParams eventParams)
        {
            RaisedEvents[(int)eventType].Enqueue(eventParams);
        }

        public IEnumerable<EventParams> GetRaisedEvents(EventType eventType)
        {
            return ProcessingEvents[(int)eventType];
        }
    }
}