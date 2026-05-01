using System;

namespace UNet.Core.Abstractions
{
    /// <summary>
    /// Расширенный <see cref="IServiceProvider"/> с поддержкой ключей (keyed services).
    /// </summary>
    public interface IKeyedServiceProvider : IServiceProvider, IDisposable
    {
        /// <summary>
        /// Получает сервис по указанному типу и ключу.
        /// </summary>
        /// <param name="type">Тип запрашиваемого сервиса.</param>
        /// <param name="key">Ключ сервиса. Если передана пустая строка — возвращается любой подходящий сервис.</param>
        /// <returns>Экземпляр сервиса или null, если сервис не найден.</returns>
        object GetKeyedService(Type type, string key);
    }

    public static class KServiceProviderExtensions
    {
        #region GetService

        public static T GetService<T>(this IKeyedServiceProvider serviceProvider) where T : class
        {
            return serviceProvider.GetService(typeof(T)) as T;
        }

        public static T GetRequiredService<T>(this IKeyedServiceProvider serviceProvider) where T : class
        {
            return serviceProvider.GetService(typeof(T)) as T ?? throw new Exception($"Could not find service of type {typeof(T)}.");
        }

        public static object GetRequiredService(this IKeyedServiceProvider serviceProvider, Type type)
        {
            return serviceProvider.GetService(type) ?? throw new Exception($"Could not find service of type {type}.");
        }

        #endregion

        #region GetKeyedService

        /// <summary>
        /// Получает сервис по типу и ключу.
        /// </summary>
        /// <typeparam name="T">Тип запрашиваемого сервиса.</typeparam>
        /// <param name="serviceProvider">Провайдер сервисов.</param>
        /// <param name="key">Ключ сервиса.</param>
        /// <returns>Экземпляр сервиса или null, если не найден.</returns>
        public static T GetKeyedService<T>(this IKeyedServiceProvider serviceProvider, string key)
            where T : class
        {
            return serviceProvider.GetKeyedService(typeof(T), key) as T;
        }

        /// <summary>
        /// Получает обязательный сервис по типу и ключу.
        /// </summary>
        /// <typeparam name="T">Тип запрашиваемого сервиса.</typeparam>
        /// <param name="serviceProvider">Провайдер сервисов.</param>
        /// <param name="key">Ключ сервиса.</param>
        /// <returns>Экземпляр сервиса.</returns>
        /// <exception cref="InvalidOperationException">
        /// Сервис типа T с указанным ключом не найден.
        /// </exception>
        public static T GetKeyedRequiredService<T>(this IKeyedServiceProvider serviceProvider, string key)
            where T : class
        {
            return serviceProvider.GetKeyedService(typeof(T), key) as T
                ?? throw new InvalidOperationException($"Could not find service of type {typeof(T)} with key '{key}'.");
        }

        /// <summary>
        /// Получает обязательный сервис по типу и ключу (не-генерик версия).
        /// </summary>
        /// <param name="serviceProvider">Провайдер сервисов.</param>
        /// <param name="type">Тип запрашиваемого сервиса.</param>
        /// <param name="key">Ключ сервиса.</param>
        /// <returns>Экземпляр сервиса.</returns>
        /// <exception cref="InvalidOperationException">
        /// Сервис указанного типа с указанным ключом не найден.
        /// </exception>
        public static object GetKeyedRequiredService(this IKeyedServiceProvider serviceProvider, Type type, string key)
        {
            return serviceProvider.GetKeyedService(type, key)
                ?? throw new InvalidOperationException($"Could not find service of type {type} with key '{key}'.");
        }

        #endregion
    }
}