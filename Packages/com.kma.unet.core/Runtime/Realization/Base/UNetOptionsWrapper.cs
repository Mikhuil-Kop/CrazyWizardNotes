using System.Linq;
using UNet.Core.Abstractions;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UNet.Core.Realization
{
    /// <summary>
    /// Базовый класс в UNet. Используется вместо добавления компонентов напрямую.
    /// </summary>
    public class UNetOptionsWrapper : MonoBehaviour, IOptionsWrapper
    {
        [SerializeField]
        private Object[] Options;

        object[] IOptionsWrapper.GetOptions()
        {
            return Options.ToArray();
        }
    }
}