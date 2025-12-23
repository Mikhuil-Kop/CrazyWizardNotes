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
            var types = GetWrappedTypes();

            foreach (var type in types)
            {
                serviceProvider.Add(type, null, type);
            }

            return types.Select(x => serviceProvider.GetService(x)).ToArray();
        }

        public abstract IEnumerable<Type> GetWrappedTypes();
    }
}
