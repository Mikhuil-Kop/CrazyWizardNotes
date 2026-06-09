using System;
using System.Threading.Tasks;

namespace UNet.Core.Abstractions
{
    /// <summary>
    /// Сервис для работы с асинхронными задачами. Позволяет регистрировать задачи, которые будут выполняться с определённым интервалом времени, или запускать задачи с фиксированным временем выполнения.
    /// </summary>
    public interface IAsyncJobService
    {
        /// <summary>
        /// Регистрировать асинхронную задачу, которая будет повторяться через каждый заданный промежуток времени.
        /// </summary>
        /// <param name="job"></param>
        IRegistredJob Register(AsyncJobParams job);
    }

    public static class AsyncJobServiceExtensions
    {
        public static IRegistredJob Register(this IAsyncJobService asyncJobService, Func<Task> func, float repeatTime, int? repeatCount = null, AsyncJobWaitType waitType = AsyncJobWaitType.None)
        {
            return asyncJobService.Register(new AsyncJobParams
            {
                AsyncTask = func,
                InitialDelay = 0,
                RepeatTime = repeatTime,
                RepeatCount = repeatCount,
                WaitType = waitType
            });
        }

        public static IRegistredJob Register(this IAsyncJobService asyncJobService, Action func, float repeatTime, int? repeatCount = null, AsyncJobWaitType waitType = AsyncJobWaitType.None)
        {
            return asyncJobService.Register(new AsyncJobParams
            {
                NormalTask = func,
                InitialDelay = 0,
                RepeatTime = repeatTime,
                RepeatCount = repeatCount,
                WaitType = waitType
            });
        }

        public static IRegistredJob RegisterFixed(this IAsyncJobService asyncJobService, Action func, float repeatTime, int? repeatCount = null, AsyncJobWaitType waitType = AsyncJobWaitType.None)
        {
            return asyncJobService.Register(new AsyncJobParams
            {
                FixedTask = func,
                InitialDelay = 0,
                RepeatTime = repeatTime,
                RepeatCount = repeatCount,
                WaitType = waitType
            });
        }

        public static IRegistredJob ExecuteDelayed(this IAsyncJobService asyncJobService, Func<Task> func, float delayTime, int repeatCount = 1)
        {
            return asyncJobService.Register(new AsyncJobParams
            {
                AsyncTask = func,
                InitialDelay = delayTime,
                RepeatTime = float.MaxValue,
                RepeatCount = repeatCount,
                WaitType = AsyncJobWaitType.None
            });
        }

        public static IRegistredJob ExecuteDelayed(this IAsyncJobService asyncJobService, Action func, float delayTime, int repeatCount = 1)
        {
            return asyncJobService.Register(new AsyncJobParams
            {
                NormalTask = func,
                InitialDelay = delayTime,
                RepeatTime = float.MaxValue,
                RepeatCount = repeatCount,
                WaitType = AsyncJobWaitType.None
            });
        }
    }
}