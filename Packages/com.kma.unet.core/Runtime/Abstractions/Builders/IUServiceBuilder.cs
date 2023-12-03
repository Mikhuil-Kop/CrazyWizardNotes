using System;

namespace UNet.Core.Abstractions
{
    public interface IUServiceBuilder
    {
        void Add<T>(ServiceScopeType serviceScopeType, Func<IServiceProvider, object> factory);
        void Add<T>(ServiceScopeType serviceScopeType, Type originalType);
        void Add<T>(ServiceScopeType serviceScopeType, string key, Func<IServiceProvider, object> factory);
        void Add<T>(ServiceScopeType serviceScopeType, string key, Type originalType);
    }

    public static class ServiceBuilderExtensions
    {
        #region Builder

        public static void Add<T1, T2>(this IUServiceBuilder serviceBuilder, ServiceScopeType scopeType) where T2 : class, T1
        {
            serviceBuilder.Add<T1>(scopeType, typeof(T2));
        }

        #endregion

        #region Transient

        public static void AddPerCall<T1>(this IUServiceBuilder serviceBuilder) where T1 : class
        {
            serviceBuilder.Add<T1>(ServiceScopeType.Transient, typeof(T1));
        }

        public static void AddPerCall<T1, T2>(this IUServiceBuilder serviceBuilder) where T2 : class, T1
        {
            serviceBuilder.Add<T1>(ServiceScopeType.Transient, typeof(T2));
        }

        public static void AddPerCall<T1>(this IUServiceBuilder serviceBuilder, Func<IServiceProvider, T1> factory)
        {
            serviceBuilder.Add<T1>(ServiceScopeType.Transient, s => factory.Invoke(s));
        }

        public static void AddPerCall<T1>(this IUServiceBuilder serviceBuilder, string key) where T1 : class
        {
            serviceBuilder.Add<T1>(ServiceScopeType.Transient, key, typeof(T1));
        }

        public static void AddPerCall<T1, T2>(this IUServiceBuilder serviceBuilder, string key) where T2 : class, T1
        {
            serviceBuilder.Add<T1>(ServiceScopeType.Transient, key, typeof(T2));
        }

        public static void AddPerCall<T1>(this IUServiceBuilder serviceBuilder, string key, Func<IServiceProvider, T1> factory)
        {
            serviceBuilder.Add<T1>(ServiceScopeType.Transient, key, s => factory.Invoke(s));
        }

        #endregion

        #region Component

        public static void AddPerComponent<T1>(this IUServiceBuilder serviceBuilder) where T1 : class
        {
            serviceBuilder.Add<T1>(ServiceScopeType.Component, typeof(T1));
        }

        public static void AddPerComponent<T1, T2>(this IUServiceBuilder serviceBuilder) where T2 : class, T1
        {
            serviceBuilder.Add<T1>(ServiceScopeType.Component, typeof(T2));
        }

        public static void AddPerComponent<T1>(this IUServiceBuilder serviceBuilder, Func<IServiceProvider, T1> factory)
        {
            serviceBuilder.Add<T1>(ServiceScopeType.Component, s => factory.Invoke(s));
        }

        public static void AddPerComponent<T1>(this IUServiceBuilder serviceBuilder, string key) where T1 : class
        {
            serviceBuilder.Add<T1>(ServiceScopeType.Component, key, typeof(T1));
        }

        public static void AddPerComponent<T1, T2>(this IUServiceBuilder serviceBuilder, string key) where T2 : class, T1
        {
            serviceBuilder.Add<T1>(ServiceScopeType.Component, key, typeof(T2));
        }

        public static void AddPerComponent<T1>(this IUServiceBuilder serviceBuilder, string key, Func<IServiceProvider, T1> factory)
        {
            serviceBuilder.Add<T1>(ServiceScopeType.Component, key, s => factory.Invoke(s));
        }

        #endregion

        #region Actor

        public static void AddPerActor<T1>(this IUServiceBuilder serviceBuilder) where T1 : class
        {
            serviceBuilder.Add<T1>(ServiceScopeType.Actor, typeof(T1));
        }

        public static void AddPerActor<T1, T2>(this IUServiceBuilder serviceBuilder) where T2 : class, T1
        {
            serviceBuilder.Add<T1>(ServiceScopeType.Actor, typeof(T2));
        }

        public static void AddPerActor<T1>(this IUServiceBuilder serviceBuilder, Func<IServiceProvider, T1> factory)
        {
            serviceBuilder.Add<T1>(ServiceScopeType.Actor, s => factory.Invoke(s));
        }

        public static void AddPerActor<T1>(this IUServiceBuilder serviceBuilder, string key) where T1 : class
        {
            serviceBuilder.Add<T1>(ServiceScopeType.Actor, key, typeof(T1));
        }

        public static void AddPerActor<T1, T2>(this IUServiceBuilder serviceBuilder, string key) where T2 : class, T1
        {
            serviceBuilder.Add<T1>(ServiceScopeType.Actor, key, typeof(T2));
        }

        public static void AddPerActor<T1>(this IUServiceBuilder serviceBuilder, string key, Func<IServiceProvider, T1> factory)
        {
            serviceBuilder.Add<T1>(ServiceScopeType.Actor, key, s => factory.Invoke(s));
        }

        #endregion

        #region Scene

        public static void AddPerScene<T1>(this IUServiceBuilder serviceBuilder) where T1 : class
        {
            serviceBuilder.Add<T1>(ServiceScopeType.Scene, typeof(T1));
        }

        public static void AddPerScene<T1, T2>(this IUServiceBuilder serviceBuilder) where T2 : class, T1
        {
            serviceBuilder.Add<T1>(ServiceScopeType.Scene, typeof(T2));
        }

        public static void AddPerScene<T1>(this IUServiceBuilder serviceBuilder, Func<IServiceProvider, T1> factory)
        {
            serviceBuilder.Add<T1>(ServiceScopeType.Scene, s => factory.Invoke(s));
        }

        public static void AddPerScene<T1>(this IUServiceBuilder serviceBuilder, string key) where T1 : class
        {
            serviceBuilder.Add<T1>(ServiceScopeType.Scene, key, typeof(T1));
        }

        public static void AddPerScene<T1, T2>(this IUServiceBuilder serviceBuilder, string key) where T2 : class, T1
        {
            serviceBuilder.Add<T1>(ServiceScopeType.Scene, key, typeof(T2));
        }

        public static void AddPerScene<T1>(this IUServiceBuilder serviceBuilder, string key, Func<IServiceProvider, T1> factory)
        {
            serviceBuilder.Add<T1>(ServiceScopeType.Scene, key, s => factory.Invoke(s));
        }

        #endregion

        #region Application

        public static void AddPerApplication<T1>(this IUServiceBuilder serviceBuilder) where T1 : class
        {
            serviceBuilder.Add<T1>(ServiceScopeType.Application, typeof(T1));
        }

        public static void AddPerApplication<T1, T2>(this IUServiceBuilder serviceBuilder) where T2 : class, T1
        {
            serviceBuilder.Add<T1>(ServiceScopeType.Application, typeof(T2));
        }

        public static void AddPerApplication<T1>(this IUServiceBuilder serviceBuilder, Func<IServiceProvider, T1> factory)
        {
            serviceBuilder.Add<T1>(ServiceScopeType.Application, s => factory.Invoke(s));
        }

        public static void AddPerApplication<T1>(this IUServiceBuilder serviceBuilder, string key) where T1 : class
        {
            serviceBuilder.Add<T1>(ServiceScopeType.Application, key, typeof(T1));
        }

        public static void AddPerApplication<T1, T2>(this IUServiceBuilder serviceBuilder, string key) where T2 : class, T1
        {
            serviceBuilder.Add<T1>(ServiceScopeType.Application, key, typeof(T2));
        }

        public static void AddPerApplication<T1>(this IUServiceBuilder serviceBuilder, string key, Func<IServiceProvider, T1> factory)
        {
            serviceBuilder.Add<T1>(ServiceScopeType.Application, key, s => factory.Invoke(s));
        }

        #endregion
    }
}