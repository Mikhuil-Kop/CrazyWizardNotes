using System;

namespace UNet.Core.Abstractions
{
    public interface ILogger
    {
        public void LogInfo(string message);
        public void LogWarning(string message);
        public void LogWarning(Exception message);
        public void LogError(string message);
        public void LogError(Exception message);
    }
}