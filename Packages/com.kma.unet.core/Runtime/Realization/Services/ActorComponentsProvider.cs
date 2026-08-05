using System;
using System.Collections.Generic;
using UNet.Core.Abstractions;

namespace UNet.Core.Realization
{
    public class ActorComponentsProvider : IActorComponentsProvider
    {
        public List<Func<IUServiceProvider, object>> Components { get; } = new List<Func<IUServiceProvider, object>>();

        public void AddComponent(Func<IUServiceProvider, object> selector)
        {
            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            Components.Add(selector);
        }
    }
}