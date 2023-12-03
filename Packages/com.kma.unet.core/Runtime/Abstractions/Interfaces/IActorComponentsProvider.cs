using System;

namespace UNet.Core.Abstractions
{
    public interface IActorComponentsProvider
    {
        void AddComponent(Func<IUServiceProvider, object> selector);
    }

    public static class ActorComponentsProviderExtensions
    {
        public static void AddComponent<T>(this IActorComponentsProvider provider) where T : class
        {
            provider.AddComponent(x => x.GetRequiredService<T>());
        }

        public static void AddComponent<T>(this IActorComponentsProvider provider, string key) where T : class
        {
            provider.AddComponent(x => x.GetKeyedRequiredService<T>(key));
        }

        public static void AddComponent(this IActorComponentsProvider provider, Type type)
        {
            provider.AddComponent(x => x.GetRequiredService(type));
        }

        public static void AddComponent(this IActorComponentsProvider provider, Type type, string key)
        {
            provider.AddComponent(x => x.GetKeyedRequiredService(type, key));
        }
    }
}