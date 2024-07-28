using UNet.Core.Abstractions;
using UnityEngine;

namespace UNet.Core.Realization
{

    /// <summary>
    /// Базовый класс в UNet. Используется вместо добавления компонентов напрямую.
    /// </summary>
    public class UNetComponentWrapper : MonoBehaviour, IComponentsWrapper
    {
        [SerializeField]
        private string ComponentName;
        [SerializeField]
        private Object ComponentObject;
        private object Component { get; set; }

        public object[] GetComponents(IUServiceProvider serviceProvider)
        {
            if (Component == null)
            {
                var name = string.IsNullOrWhiteSpace(ComponentName)
                    ? ComponentObject.name
                    : ComponentName;

                Component = serviceProvider.GetKeyedRequiredService<object>(name);
            }

            return new[] { Component };
        }
    }
}