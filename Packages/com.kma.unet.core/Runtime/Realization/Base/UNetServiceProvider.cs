using System;
using System.Collections.Generic;
using System.Linq;
using UNet.Core.Abstractions;
using Logger = UnityEngine.Debug;

namespace UNet.Core.Realization
{
    public class UNetServiceProvider : IUServiceProvider
    {
        private ServiceScopeType Scope { get; }
        private UNetServiceProvider ParentServiceProvider { get; }
        private LinkedList<UNetServiceSetting> Factories { get; }
		private Stack<CreatedService> CreatedServices { get; }

        public UNetServiceProvider(ServiceScopeType scope, UNetServiceProvider serviceProvider, IEnumerable<UNetServiceSetting> factories)
        {
            Scope = scope;
            ParentServiceProvider = serviceProvider;
            Factories = factories?.ToLinkedList() ?? serviceProvider?.Factories.ToLinkedList();
            CreatedServices = new Stack<CreatedService>();

            if (Factories == null)
            {
                throw new Exception("UNetServiceProvider must be provided with factories via constructor or parent service provider.");
            }

            if (Factories.Any(x => (x.Factory == null) && (x.OriginalType == null)))
            {
                throw new Exception("UNetServiceSetting must provide OriginalType or Factory.");
            }

            foreach (var factory in Factories.Where(x => x.Factory == null))
            {
                factory.Factory = GetDefaultFactory(factory.OriginalType);
            }
        }

        public IUServiceProvider BeginScope(ServiceScopeType scopeType)
        {
            if (Scope != ServiceScopeType.Application && scopeType != ServiceScopeType.Application && (byte)scopeType != ((byte)Scope + 1))
            {
                throw new ArgumentException($"Cannot create Scope with type {scopeType}. Expecting {Scope + 1}.", nameof(scopeType));
            }

            return new UNetServiceProvider(scopeType, this, null);
        }

        public void Add(Type targetType, string key, Func<IKeyedServiceProvider, object> factory)
        {
            var setting = new UNetServiceSetting
            {
                ServiceScopeType = Scope,
                Factory = factory,
                OriginalType = null,
                TargetType = targetType,
                Key = key,
            };

            Factories.AddFirst(setting);

            Logger.Log($"Added object of type \"{targetType.Name}\".");
        }

        public void Add(Type targetType, string key, Type originalType)
        {
            var setting = new UNetServiceSetting
            {
                ServiceScopeType = Scope,
                Factory = GetDefaultFactory(originalType),
                OriginalType = originalType,
                TargetType = targetType,
                Key = key,
            };

            Factories.AddFirst(setting);

            Logger.Log($"Added object of type \"{targetType.Name}\".");
        }

        public IEnumerable<UNetServiceProviderItem> GetServicesInfo()
        {
            return Factories.Select(x => new UNetServiceProviderItem
            {
                OriginalType = x.OriginalType,
                TargetType = x.TargetType,
                Key = x.Key,
                IsCreated = CreatedServices.Any(y => (y.Key == x.Key || x.Key == null) && x.TargetType.IsAssignableFrom(y.Service.GetType()))
            });
        }

        public object GetService(Type serviceType)
        {
            return InternalGetService(serviceType, null);
        }

        public object GetKeyedService(Type serviceType, string key)
        {
            return InternalGetService(serviceType, key);
        }

        private object InternalGetService(Type serviceType, string key)
        {
            try
            {
                if (serviceType == null)
                {
                    throw new ArgumentNullException("serviceType");
                }

                if (serviceType.IsAssignableFrom(typeof(IUServiceProvider)))
                {
                    return this;
                }

                //// Костыль. Перенести
                if (serviceType.IsGenericType && serviceType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {

                    var elementType = serviceType.GetGenericArguments()[0];
                    var service = InternalGetService(elementType, key);
                    var array = Array.CreateInstance(elementType, 1);

                    array.SetValue(service, 0);
                    return array;

                }

                var existingService = CreatedServices.FirstOrDefault(x => (x.Key == key || key == null) && serviceType.IsAssignableFrom(x.Service.GetType()));

                if (existingService != null)
                {
                    return existingService.Service;
                }

                var setting = Factories.FirstOrDefault(x => (x.Key == key || key == null) && serviceType.IsAssignableFrom(x.TargetType));

                if (setting == null)
                {
                    throw new KeyNotFoundException($"No registred service for \"{serviceType.FullName}\" and key \"{key}\".");
                }

                // Можно оптимизировать поиск
                if (setting.ServiceScopeType == Scope)
                {
                    Logger.Log($"Creating service of type \"{serviceType.FullName}\" with key \"{key}\".");

                    var service = setting.Factory.Invoke(this);

                    Logger.Log($"Created service of type \"{serviceType.FullName}\" with key \"{key}\". ({service.GetType()})");

                    CreatedServices.Push(new CreatedService()
                    {
                        Key = setting.Key,
                        Service = service,
                    });

                    return service;
                }
                else if (setting.ServiceScopeType < Scope)
                {
                    var provider = this.ToRecursion(x => x.ParentServiceProvider).First(x => x.Scope == setting.ServiceScopeType);

                    return provider.InternalGetService(serviceType, key);
                }
                else
                {
                    throw new KeyNotFoundException($"No registred service for \"{serviceType.FullName}\" and key \"{key}\". \"{setting.ServiceScopeType}\" > \"{Scope}\".");
                }
            }
            catch (StackOverflowException)
            {
                throw new Exception($"Не удалось получить {serviceType.FullName} из-за циклической зависимости.");
            }
            catch(Exception e)
            {
                throw new ServiceNotProvidedException(serviceType?.Name ?? "null", key, e);
            }
        }

        public void Dispose()
        {
            foreach (var service in CreatedServices)
            {
                (service.Service as IDisposable)?.Dispose();
            }
        }

        private Func<IKeyedServiceProvider, object> GetDefaultFactory(Type type)
        {
            var constuctor = type.GetConstructors().SingleOrDefault()
                ?? throw new Exception($"Сервис {type.FullName} должен иметь только один конструктор.");

            var parameters = constuctor.GetParameters().Select(x => new
            {
                Type = x.ParameterType,
                Key = x.GetCustomAttributes(typeof(FromKeyAttribute), true).Select(x => x as FromKeyAttribute).FirstOrDefault(x => x != null)?.Key
            }).ToArray();

            if (parameters.Any(x => x.Type.IsAssignableFrom(type)))
            {
                throw new Exception($"В конструкторе {constuctor.DeclaringType} обнаружена циклическая зависимость.");
            }

            return (s) => constuctor.Invoke(parameters.Select(x => s.GetKeyedService(x.Type, x.Key)).ToArray());
        }

        private class CreatedService
        {
            public string Key { get; set; }
            public object Service { get; set; }
        }

        public class UNetServiceProviderItem
        {
            public Type TargetType { get; set; }
            public Type OriginalType { get; set; }
            public string Key { get; set; }
            public bool IsCreated { get; set; }
        }
    }
}