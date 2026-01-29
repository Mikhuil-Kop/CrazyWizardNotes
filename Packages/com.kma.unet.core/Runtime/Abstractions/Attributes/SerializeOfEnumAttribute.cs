using System;

namespace UNet.Core.Abstractions
{
    public class SerializeOfEnumAttribute : Attribute
    {
        public readonly Type EnumType;
        public SerializeOfEnumAttribute(Type enumType)
        {
            EnumType = enumType;
        }
    }
}
