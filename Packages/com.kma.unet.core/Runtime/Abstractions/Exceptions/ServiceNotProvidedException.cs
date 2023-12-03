using System;

namespace UNet.Core.Abstractions
{
    public class ServiceNotProvidedException : Exception
    {
        public string ServiceName { get; }
        public string ServiceKey { get; }

        public ServiceNotProvidedException(string serviceName, string serviceKey, Exception innerException) : base(null, innerException)
        {
            ServiceName = serviceName;
            ServiceKey = serviceKey;
        }

        public override string Message => $"Failed to create service {GetShortMessage()}";

        private string GetShortMessage()
        {
            if (InnerException == null)
            {
                return $"\"{ServiceName}\"[{ServiceKey}]";
            }
            else if (InnerException is ServiceNotProvidedException e)
            {
                return $"\"{ServiceName}\"[{ServiceKey}] - {e.GetShortMessage()}";
            }
            else
            {
                return $"\"{ServiceName}\"[{ServiceKey}] --- {InnerException.Message}";
            }
        }
    }
}
