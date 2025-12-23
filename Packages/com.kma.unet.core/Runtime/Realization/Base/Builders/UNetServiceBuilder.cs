using System;
using System.Collections.Generic;
using UNet.Core.Realization;

namespace UNet.Core.Abstractions
{
    public class UNetServiceBuilder : IUServiceBuilder
    {
        private Stack<UNetServiceSetting> Factories { get; set; } = new Stack<UNetServiceSetting>();

        public void Add<T>(ServiceScopeType serviceScopeType, Func<IServiceProvider, object> factory)
        {
            Add<T>(serviceScopeType, null, factory);
        }

        public void Add<T>(ServiceScopeType serviceScopeType, Type originalType)
        {
            Add<T>(serviceScopeType, null, originalType);
        }

        public void Add<T>(ServiceScopeType serviceScopeType, string key, Func<IServiceProvider, object> factory)
        {
            var setting = new UNetServiceSetting()
            {
                ServiceScopeType = serviceScopeType,
                TargetType = typeof(T),
                OriginalType = null,
                Factory = factory ?? throw new ArgumentNullException("factory"),
                Key = key ?? GetDefaultKey<T>()
            };

            Factories.Push(setting);
        }

        public void Add<T>(ServiceScopeType serviceScopeType, string key, Type originalType)
        {
            var setting = new UNetServiceSetting()
            {
                ServiceScopeType = serviceScopeType,
                TargetType = typeof(T),
                OriginalType = originalType ?? throw new ArgumentNullException("originalType"),
                Factory = null,
                Key = key ?? GetDefaultKey<T>()
            };

            Factories.Push(setting);
        }

        public IUServiceProvider Build()
		{
            return new UNetServiceProvider(ServiceScopeType.Application, null, Factories);
		}

        private string GetDefaultKey<T>()
        {
            return typeof(T).Name;
        }
    }
}