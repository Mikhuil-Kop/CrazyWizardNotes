using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UNet.Core.Abstractions;

namespace UNet.Core.Realization
{
    public sealed class AsyncExecutionService : IAsyncExecutionService, IHandlesUpdate
    {
        private ILogger Logger { get; }
        private List<TrackedTask> Tasks { get; }

        public AsyncExecutionService(ILogger logger)
        {
            Logger = logger;
            Tasks = new List<TrackedTask>();
        }

        public void ExecuteWithTimeoutWait(Func<CancellationToken, Task> task, int millisecondsBeforeWait)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            if (millisecondsBeforeWait < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(millisecondsBeforeWait));
            }

            var running = task(CancellationToken.None);

            running.ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    Logger.LogError($"AsyncExecutionService: ошибка в задаче с ожиданием: {t.Exception!.Flatten()}");
                }
            }, TaskScheduler.Default);

            Tasks.Add(new TrackedTask
            {
                Task = running,
                WaitAfterMs = millisecondsBeforeWait,
                StartTicks = DateTime.UtcNow.Ticks,
                IsCancelType = false
            });
        }

        public void ExecuteWithTimeoutCancel(Func<CancellationToken, Task> task, int millisecondsBeforeCancel)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            if (millisecondsBeforeCancel <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(millisecondsBeforeCancel));
            }

            var cts = new CancellationTokenSource(millisecondsBeforeCancel); // отмена через указанное время
            var running = task(cts.Token);

            running.ContinueWith(t =>
            {
                cts.Dispose();

                if (t.IsFaulted)
                {
                    Logger.LogError($"AsyncExecutionService: ошибка в задаче с ожиданием: {t.Exception!.Flatten()}");
                }
                else if (t.IsCanceled)
                {
                    Logger.LogInfo($"AsyncExecutionService: задача отменена по таймауту {millisecondsBeforeCancel} мс");
                }

            }, TaskScheduler.Default);

            Tasks.Add(new TrackedTask
            {
                Task = running,
                WaitAfterMs = millisecondsBeforeCancel,
                StartTicks = DateTime.UtcNow.Ticks,
                IsCancelType = true
            });
        }

        public void OnUpdate(float deltaTime)
        {
            var now = DateTime.UtcNow.Ticks;

            for (var i = Tasks.Count - 1; i >= 0; i--)
            {
                var t = Tasks[i];
                var elapsedMs = (now - t.StartTicks) / TimeSpan.TicksPerMillisecond;

                if (elapsedMs >= t.WaitAfterMs)
                {
                    if (t.IsCancelType)
                    {
                        // Ничего не делаем — задача уже получила CancelRequest через токен
                        // Просто убираем из списка
                    }
                    else
                    {
                        // Именно здесь и только здесь — принудительный Wait()
                        try
                        {
                            t.Task.ConfigureAwait(false).GetAwaiter().GetResult();
                        }
                        catch (Exception ex)
                        {
                            Logger.LogError(ex);
                        }
                    }

                    Tasks.RemoveAt(i);
                }
            }
        }

        private sealed class TrackedTask
        {
            public Task Task;
            public int WaitAfterMs;
            public long StartTicks;
            public bool IsCancelType; // false = Wait, true = Cancel
        }
    }
}