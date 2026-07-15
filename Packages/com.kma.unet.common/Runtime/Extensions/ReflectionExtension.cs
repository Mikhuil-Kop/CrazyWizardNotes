using System.Collections.Generic;
using System.Reflection;

namespace UNet
{
    public static class ReflectionExtension
    {
        /// <summary>
        /// Превращает все поля объекта в словарь, где ключ - имя поля, а значение - значение поля.
        /// </summary>
        /// <param name="obj">Объект, поля которого нужно преобразовать в словарь.</param>
        /// <returns>Словарь, содержащий поля объекта.</returns>
        public static Dictionary<string, object> ReflectToDictionary(this object obj)
        {
            var dict = new Dictionary<string, object>();
            if (obj == null) return dict;

            var type = obj.GetType();
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var field in fields)
            {
                dict[field.Name] = field.GetValue(obj);
            }
            return dict;
        }
    }
}
