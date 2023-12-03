using System;

namespace UNet.Core.Abstractions
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class FromKeyAttribute : Attribute
    {
        public readonly string Key;

        public FromKeyAttribute(string key)
        {
            Key = key;
        }
    }
}
