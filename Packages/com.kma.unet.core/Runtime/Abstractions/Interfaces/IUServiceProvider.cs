using System;

namespace UNet.Core.Abstractions
{
    /// <summary>
    /// Расширенный <see cref="IServiceProvider"/> с поддержкой областей видимости (scoped services).
    /// </summary>
    public interface IUServiceProvider : IKeyedServiceProvider, IDisposable
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
        void Add(Type targetType, string key, Func<IKeyedServiceProvider, object> factory);
    }

    public static class UServiceProviderExtensions
    {
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