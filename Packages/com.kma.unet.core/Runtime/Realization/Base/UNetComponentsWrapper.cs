using System.Linq;
using UNet.Core.Abstractions;
using UnityEngine;

namespace UNet.Core.Realization
{
    /// <summary>
    /// Базовый класс в UNet. Используется вместо добавления компонентов напрямую.
    /// </summary>
    public class UNetComponentsWrapper : MonoBehaviour, IComponentsWrapper
    {
        [SerializeField]
        private string[] ComponentsNames;
        [SerializeField]
        private Object[] ComponentsObjects;
        private object[] Components { get; set; }

        public object[] GetComponents(IUServiceProvider serviceProvider)
        {
            if (Components == null)
            {
                var names = ComponentsNames.Concat(ComponentsObjects.Select(x => x.name));

                Components = names.Select(x => serviceProvider.GetKeyedRequiredService<object>(x)).ToArray();
            }

            return Components;
        }
    }
}