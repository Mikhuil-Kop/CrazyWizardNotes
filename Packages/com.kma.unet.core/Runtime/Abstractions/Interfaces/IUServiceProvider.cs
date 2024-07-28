using System;

namespace UNet.Core.Abstractions
{
    public interface IUServiceProvider : IServiceProvider, IDisposable
    {
        IUServiceProvider BeginScope(ServiceScopeType scopeType);
        object GetKeyedService(Type type, string key);
        void Add(Type targetType, Func<IServiceProvider, object> factory);
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

        public static T GetKeyedService<T>(this IUServiceProvider serviceProvider, string key) where T : class
        {
            return serviceProvider.GetKeyedService(typeof(T), key) as T;
        }

        public static T GetKeyedRequiredService<T>(this IUServiceProvider serviceProvider, string key) where T : class
        {
            return serviceProvider.GetKeyedService(typeof(T), key) as T ?? throw new Exception($"Could not find service of type {typeof(T)} with {key}.");
        }

        public static object GetKeyedRequiredService(this IUServiceProvider serviceProvider, Type type, string key)
        {
            return serviceProvider.GetKeyedService(type, key) ?? throw new Exception($"Could not find service of type {type} with {key}.");
        }

        #endregion

        #region Add

        public static void Add(this IUServiceProvider serviceProvider, Type targetType, object obj)
        {
            serviceProvider.Add(targetType, (s) => obj);
        }

        public static void Add<T>(this IUServiceProvider serviceProvider, Func<IServiceProvider, object> factory)
        {
            serviceProvider.Add(typeof(T), factory);
        }

        public static void Add(this IUServiceProvider serviceProvider, object obj)
        {
            serviceProvider.Add(obj.GetType(), (s) => obj);
        }

        public static void Add<T, V>(this IUServiceProvider serviceProvider, V obj) where V : T
        {
            serviceProvider.Add(typeof(T), (s) => obj);
        }

        #endregion
    }
}