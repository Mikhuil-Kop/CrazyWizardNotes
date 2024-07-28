using System;

namespace UNet.Core.Abstractions
{
    public interface ISchedulerService
    {
        void Shedule(float delay, Action action);
    }
}