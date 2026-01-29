using System;
using System.Collections.Generic;
using System.Linq;
using UNet.Core.Abstractions;
using UnityEngine;

namespace CrazyWizardNotes.Main.Wrappers
{
    public abstract class TypedWrapperBase : MonoBehaviour, IComponentsWrapper
    {
        public object[] GetComponents(IUServiceProvider serviceProvider)
        {
            var objects = GetObjects();
            var types = GetWrappedTypes();
            var keyedObjects = GetKeyedObjects();
            var keyedTypes = GetKeyedWrappedTypes();

            foreach (var o in objects)
            {
                serviceProvider.Add(null, o);
            }

            foreach (var t in types)
            {
                serviceProvider.Add(t, null, t);
            }

            foreach (var o in keyedObjects)
            {
                serviceProvider.Add(o.Key, o.Value);
            }

            foreach (var t in keyedTypes)
            {
                serviceProvider.Add(t.Value, t.Key, t.Value);
            }

            serviceProvider.Add(typeof(IComponentsWrapper), null, GetType());

            var objectsComponents = objects.Select(x => x);
            var typesComponents = types.Select(x => serviceProvider.GetRequiredService(x));
            var keyedObjectsComponents = keyedObjects.Select(x => x.Value);
            var keyedTypesComponents = keyedTypes.Select(x => serviceProvider.GetKeyedRequiredService(x.Value, x.Key));

            return objectsComponents
                .Concat(typesComponents)
                .Concat(keyedObjectsComponents)
                .Concat(keyedTypesComponents)
                .ToArray();
        }

        protected virtual IEnumerable<object> GetObjects()
        {
            return new object[0];
        }

        protected virtual IEnumerable<Type> GetWrappedTypes()
        {
            return new Type[0];
        }

        protected virtual IDictionary<string, object> GetKeyedObjects()
        {
            return new Dictionary<string, object>();
        }

        protected virtual IDictionary<string, Type> GetKeyedWrappedTypes()
        {
            return new Dictionary<string, Type>();
        }
    }
}
