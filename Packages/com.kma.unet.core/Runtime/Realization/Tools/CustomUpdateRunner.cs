using System;
using System.Collections.Generic;
using System.Linq;
using UNet.Core.Abstractions;

namespace UNet.Core.Realization
{
    [Obsolete("Стремная технология. Переделать.")]
    public class CustomUpdateRunner
    {
        private List<Entry> Entities { get; }

        public CustomUpdateRunner(IEnumerable<IHandlesCustomUpdate> updates)
        {
            Entities = updates
                .Where(x => x != null)
                .Select(x => new Entry
                {
                    Target = x,
                    TimeLeft = 0
                })
                .ToList();
        }

        public void Update(float deltaTime)
        {
            foreach (var entity in Entities)
            {
                entity.TimeLeft -= deltaTime;

                if (entity.TimeLeft <= 0)
                {
                    entity.Target.OnCustomUpdate();
                    entity.TimeLeft += entity.Target.OnCustomUpdateDelay;
                }
            }
        }

        private class Entry
        {
            public IHandlesCustomUpdate Target;
            public float TimeLeft;
        }
    }
}