using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace CrazyWizardNotes.Common.Services
{
    public class VisualEffectsService : SingletonBehavior<IVisualEffectsService>, IVisualEffectsService
    {
        Dictionary<string, EffectsQueue> EffectsQueues { get; set; }

        protected override void Awake()
        {
            base.Awake();

            EffectsQueues = new Dictionary<string, EffectsQueue>();

            foreach (Transform child in transform.transform)
            {
                EffectsQueues[child.gameObject.name] = new EffectsQueue(child.transform.Cast<Transform>());
            }
        }

        private void Update()
        {
            var eventsService = EventsService.Instance;
            var wordEvents = eventsService.GetRaisedEvents(EventType.WordReaded);

            foreach (var wordEvent in wordEvents.Where(x => x.Target2 is MonoBehaviour))
            {
                //var effect = EffectsQueues["WordEffects"].Next();
                //effect.transform.position = (wordEvent.Target2 as MonoBehaviour).transform.position;
                Debug.Log("I can hear the word " + wordEvent.TextValue);
            }
        }

        private class EffectsQueue
        {
            private Transform[] Transforms { get; set; }
            private int NowIndex { get; set; }

            public EffectsQueue(IEnumerable<Transform> objects)
            {
                Transforms = objects.ToArray();
                NowIndex = 0;
            }

            public Transform Next()
            {
                return Transforms[NowIndex++ % Transforms.Length];
            }
        }
    }
}