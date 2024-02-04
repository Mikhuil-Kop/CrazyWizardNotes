using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CrazyWizardNotes.Common.Services
{
    public class EventsService : SingletonBehavior<IEventsService>, IEventsService
    {
        private Queue<EventParams>[] ProcessingEvents { get; set; }
        private Queue<EventParams>[] RaisedEvents { get; set; }

        protected override void Awake()
        {
            base.Awake();

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

        protected void LateUpdate()
        {
            foreach (var events in ProcessingEvents)
            {
                events.Clear();
            }

            var change = ProcessingEvents;
            ProcessingEvents = RaisedEvents;
            RaisedEvents = change;
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