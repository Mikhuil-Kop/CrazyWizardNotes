using System;

namespace UNet.Core.Abstractions
{
    /// <summary>
    /// Расширенный <see cref="IServiceProvider"/> с поддержкой областей видимости (scoped services).
    /// </summary>
    public interface IUServiceProvider : IServiceProvider, IDisposable
    {
        /// <summary>
        /// Создаёт новый дочерний провайдер с указанным уровнем области видимости.
        /// </summary>
        /// <param name="scopeType">Тип (уровень) требуемой области видимости.</param>
        /// <returns>Новый экземпляр провайдера с заданным уровнем области видимости.</returns>
        /// <exception cref="ArgumentException">
        /// Выбрасывается, если запрашиваемый scopeType не является дочерним по отношению к текущему уровню.
        /// </exception>
        IUServiceProvider BeginScope(ServiceScopeType scopeType);

        /// <summary>
        /// Получает сервис по указанному типу и ключу.
        /// </summary>
        /// <param name="type">Тип запрашиваемого сервиса.</param>
        /// <param name="key">Ключ сервиса. Если передана пустая строка — возвращается любой подходящий сервис.</param>
        /// <returns>Экземпляр сервиса или null, если сервис не найден.</returns>
        object GetKeyedService(Type type, string key);

        /// <summary>
        /// Регистрирует сервис с указанной реализацией. Доступен во всех дочерних областях.
        /// </summary>
        /// <param name="targetType">Тип сервиса (обычно интерфейс).</param>
        /// <param name="key">Уникальный ключ регистрации.</param>
        /// <param name="originalType">Конкретный класс, который будет создаваться.</param>
        void Add(Type targetType, string key, Type originalType);

        /// <summary>
        /// Регистрирует сервис с использованием фабричного метода. Доступен во всех дочерних областях.
        /// </summary>
        /// <param name="targetType">Тип сервиса (обычно интерфейс).</param>
        /// <param name="key">Уникальный ключ регистрации.</param>
        /// <param name="factory">Фабрика, создающая экземпляр сервиса.</param>
        void Add(Type targetType, string key, Func<IServiceProvider, object> factory);
    }

    public static class UServiceProviderExtensions
    {
        #region GetService

        public static T GetService<T>(this IUServiceProvider serviceProvider) where T : class
        {
            return serviceProvider.GetService(typeof(T)) as T;
        }

        public static T GetRequiredService<T>(this IUServiceProvider serviceProvider) where T : class
        {
            return serviceProvider.GetService(typeof(T)) as T ?? throw new Exception($"Could not find service of type {typeof(T)}.");
        }

        public static object GetRequiredService(this IUServiceProvider serviceProvider, Type type)
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
        public static T GetKeyedService<T>(this IUServiceProvider serviceProvider, string key)
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
        public static T GetKeyedRequiredService<T>(this IUServiceProvider serviceProvider, string key)
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
        public static object GetKeyedRequiredService(this IUServiceProvider serviceProvider, Type type, string key)
        {
            return serviceProvider.GetKeyedService(type, key)
                ?? throw new InvalidOperationException($"Could not find service of type {type} with key '{key}'.");
        }

        #endregion

        #region Add

        /// <summary>
        /// Регистрирует реализацию сервиса с ключом.
        /// </summary>
        /// <typeparam name="TImplementation">Конкретная реализация.</typeparam>
        /// <param name="provider">Провайдер сервисов.</param>
        /// <param name="key">Уникальный ключ регистрации.</param>
        public static void Add<TImplementation>(this IUServiceProvider provider, string key) where TImplementation : class
        {
            provider.Add(typeof(TImplementation), key, typeof(TImplementation));
        }

        /// <summary>
        /// Регистрирует реализацию сервиса с ключом.
        /// </summary>
        /// <typeparam name="TService">Тип сервиса (интерфейс или базовый класс).</typeparam>
        /// <typeparam name="TImplementation">Конкретная реализация.</typeparam>
        /// <param name="provider">Провайдер сервисов.</param>
        /// <param name="key">Уникальный ключ регистрации.</param>
        public static void Add<TService, TImplementation>(this IUServiceProvider provider, string key) where TImplementation : class, TService
        {
            provider.Add(typeof(TService), key, typeof(TImplementation));
        }

        /// <summary>
        /// Регистрирует сервис с фабрикой и ключом.
        /// </summary>
        /// <typeparam name="TService">Тип сервиса.</typeparam>
        /// <param name="provider">Провайдер сервисов.</param>
        /// <param name="key">Уникальный ключ регистрации.</param>
        /// <param name="factory">Фабрика, создающая экземпляр сервиса.</param>
        public static void Add<TService>(this IUServiceProvider provider, string key, Func<IServiceProvider, TService> factory)
        {
            provider.Add(typeof(TService), key, sp => factory(sp));
        }

        /// <summary>
        /// Регистрирует готовый экземпляр с ключом.
        /// </summary>
        /// <param name="provider">Провайдер сервисов.</param>
        /// <param name="key">Уникальный ключ регистрации.</param>
        /// <param name="obj">Экземпляр сервиса.</param>
        public static void Add(this IUServiceProvider provider, string key, object obj)
        {
            provider.Add(obj.GetType(), key, sp => obj);
        }

        /// <summary>
        /// Регистрирует реализацию сервиса без ключа.
        /// </summary>
        /// <typeparam name="TService">Тип сервиса.</typeparam>
        /// <typeparam name="TImplementation">Конкретная реализация.</typeparam>
        /// <param name="provider">Провайдер сервисов.</param>
        public static void Add<TService, TImplementation>(this IUServiceProvider provider) where TImplementation : class, TService
        {
            provider.Add(typeof(TService), null, typeof(TImplementation));
        }

        /// <summary>
        /// Регистрирует реализацию сервиса без ключа.
        /// </summary>
        /// <typeparam name="TImplementation">Конкретная реализация.</typeparam>
        /// <param name="provider">Провайдер сервисов.</param>
        public static void Add<TImplementation>(this IUServiceProvider provider) where TImplementation : class
        {
            provider.Add(typeof(TImplementation), null, typeof(TImplementation));
        }

        /// <summary>
        /// Регистрирует сервис с фабрикой без ключа.
        /// </summary>
        /// <typeparam name="TService">Тип сервиса.</typeparam>
        /// <param name="provider">Провайдер сервисов.</param>
        /// <param name="factory">Фабрика, создающая экземпляр сервиса.</param>
        public static void Add<TService>(this IUServiceProvider provider, Func<IServiceProvider, TService> factory)
        {
            provider.Add(typeof(TService), null, sp => factory(sp));
        }

        /// <summary>
        /// Регистрирует готовый экземпляр без ключа.
        /// </summary>
        /// <param name="provider">Провайдер сервисов.</param>
        /// <param name="obj">Экземпляр сервиса.</param>
        public static void Add(this IUServiceProvider provider, object obj)
        {
            provider.Add(obj.GetType(), null, sp => obj);
        }

        #endregion
    }
}