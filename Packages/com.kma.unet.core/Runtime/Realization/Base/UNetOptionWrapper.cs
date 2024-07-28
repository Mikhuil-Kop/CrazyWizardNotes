using UNet.Core.Abstractions;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UNet.Core.Realization
{
    /// <summary>
    /// Базовый класс в UNet. Используется вместо добавления компонентов напрямую.
    /// </summary>
    public class UNetOptionWrapper : MonoBehaviour, IOptionsWrapper
    {
        [SerializeField]
        private Object Options;

        object[] IOptionsWrapper.GetOptions()
        {
            if (Options != null)
            {
                return new[] { Options };
            }
            else
            {
                return new object[0];
            }
        }
    }
}