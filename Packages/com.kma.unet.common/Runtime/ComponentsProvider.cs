using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace UNet.Common
{
    public class ComponentsProvider : IRealisationProvider
    {
        private Stack<object> Components { get; set; }
        private Stack<ComponentInitializer> DefaultComponents { get; set; }

        public ComponentsProvider(MonoBehaviour monoBehaviour): this(monoBehaviour.gameObject) { }

        public ComponentsProvider(GameObject gameObject)
        {
            Components = new Stack<object>(gameObject.GetComponents<MonoBehaviour>());
            DefaultComponents = new Stack<ComponentInitializer>();
        }

        public void AddDefaultImplementation<T>(Func<T> implementation) where T : class
        {
            DefaultComponents.Push(new ComponentInitializer()
            {
                Type = typeof(T),
                Initializer = new Func<object>(() => implementation())
            });
        }

        public T Get<T>() where T : class
        {
            var component =
                Components.FirstOrDefault(x => x is T)
                ?? DefaultComponents.FirstOrDefault(x => x.Type.IsAssignableFrom(typeof(T)))?.Initializer();

            return component as T;
        }

        private class ComponentInitializer
        {
            public Type Type { get; set; }
            public Func<object> Initializer { get; set; }
        }
    }
}