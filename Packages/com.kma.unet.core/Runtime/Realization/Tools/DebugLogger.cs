using System;
using UNet.Core.Abstractions;

namespace UNet.Core.Realization
{
    public class DebugLogger : ILogger
    {
        public void LogInfo(string message)
        {
            UnityEngine.Debug.Log(message);
        }

        public void LogWarning(string message)
        {
            UnityEngine.Debug.LogWarning(message);
        }

        public void LogWarning(Exception message)
        {
            UnityEngine.Debug.LogWarning(message);
        }

        public void LogError(string message)
        {
            UnityEngine.Debug.LogError(message);
        }

        public void LogError(Exception message)
        {
            UnityEngine.Debug.LogError(message);
        }
    }
}