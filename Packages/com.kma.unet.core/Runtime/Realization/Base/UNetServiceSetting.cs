using System;
using UNet.Core.Abstractions;

namespace UNet.Core.Realization
{
    public class UNetServiceSetting
    {
        public string Key { get; set; }
        public Type TargetType { get; set; }
        public Type OriginalType { get; set; }
        public ServiceScopeType ServiceScopeType { get; set; }
        public Func<IKeyedServiceProvider, object> Factory { get; set; }
    }
}