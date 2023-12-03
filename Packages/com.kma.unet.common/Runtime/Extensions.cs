using System;

namespace UNet.Common
{
    public static class Exceptions
    {
        public static void CheckArgumentNull(object argument, string name)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(name);
            }
        }
    }
}