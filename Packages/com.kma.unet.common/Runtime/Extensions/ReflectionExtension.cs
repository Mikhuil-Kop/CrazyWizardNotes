using System;
using System.Collections.Generic;
using System.Linq;
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

        public static Type GetTypeByNameRequired(string name, bool strictOnly)
        {
            return GetTypeByName(name, strictOnly) ?? throw new KeyNotFoundException($"Type \"{name}\" not found. (strictOnly={strictOnly})");
        }

        public static Type GetTypeByName(string name, bool strictOnly)
        {
            if (strictOnly)
            {
                return
                    AppDomain.CurrentDomain.GetAssemblies()
                        .Reverse()
                        .Select(assembly => assembly.GetType(name))
                        .FirstOrDefault(t => t != null);
            }
            else
            {
                return
                    AppDomain.CurrentDomain.GetAssemblies()
                        .Reverse()
                        .Select(assembly => assembly.GetType(name))
                        .FirstOrDefault(t => t != null)
                    ??
                    AppDomain.CurrentDomain.GetAssemblies()
                        .Reverse()
                        .SelectMany(assembly => assembly.GetTypes())
                        .FirstOrDefault(t => t.Name.Contains(name));
            }
        }
    }
}
