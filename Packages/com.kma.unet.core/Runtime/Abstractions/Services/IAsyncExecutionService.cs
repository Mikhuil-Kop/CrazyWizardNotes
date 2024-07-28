using System;
using System.Threading;
using System.Threading.Tasks;

namespace UNet.Core.Abstractions
{
    /// <summary>
    /// Сервис, который отвечает за выполнение долгих асинхронных задач, результат которых можно подождать.
    /// </summary>
    public interface IAsyncExecutionService
    {
        /// <summary>
        /// Запустить выполнение задачи. Спустя заданное время ожидания, если задача не завершилась, остановить вызывающий поток до ее полного выполнения.
        /// </summary>
        /// <param name="task">Задача, которую нужно выполнить.</param>
        /// <param name="millisecondsBeforeWait">Кол-во миллисекунд на выполнение задачи. После этого времени принудительный Wait().</param>
        void ExecuteWithTimeoutWait(Func<CancellationToken, Task> task, int millisecondsBeforeWait);

        /// <summary>
        /// Запустить выполнение задачи. Спустя заданное время ожидания, если задача не завершилась, остановить вызывающий поток до ее полного выполнения.
        /// </summary>
        /// <param name="task">Задача, которую нужно выполнить.</param>
        /// <param name="millisecondsBeforeWait">Кол-во миллисекунд на выполнение задачи. После этого времени принудительная отмена.</param>
        void ExecuteWithTimeoutCancel(Func<CancellationToken, Task> task, int millisecondsBeforeCancel);
    }

    public static class AsyncExecutionServiceExtensions
    {
        /// <summary>
        /// Запустить выполнение задачи. Спустя заданное время ожидания, если задача не завершилась, остановить вызывающий поток до ее полного выполнения.
        /// </summary>
        /// <param name="service">Сервис выполнения асинхронных задач.</param>
        /// <param name="task">Задача, которую нужно выполнить (без CancellationToken).</param>
        /// <param name="millisecondsBeforeWait">Кол-во миллисекунд на выполнение задачи. После этого времени принудительный Wait().</param>
        public static void ExecuteWithTimeoutWait(this IAsyncExecutionService service, Func<Task> task, int millisecondsBeforeWait)
        {
            service.ExecuteWithTimeoutWait(_ => task(), millisecondsBeforeWait);
        }

        /// <summary>
        /// Запустить выполнение задачи. Спустя заданное время ожидания, если задача не завершилась, остановить вызывающий поток до ее полного выполнения.
        /// </summary>
        /// <param name="service">Сервис выполнения асинхронных задач.</param>
        /// <param name="task">Задача, которую нужно выполнить (без CancellationToken).</param>
        /// <param name="millisecondsBeforeWait">Кол-во миллисекунд на выполнение задачи. После этого времени принудительный Wait().</param>
        public static void ExecuteWithTimeoutWait(this IAsyncExecutionService service, Action action, int millisecondsBeforeWait)
        {
            service.ExecuteWithTimeoutWait(_ => Task.Run(action), millisecondsBeforeWait);
        }

        /// <summary>
        /// Запустить выполнение задачи. Спустя заданное время ожидания, если задача не завершилась, остановить вызывающий поток до ее полного выполнения.
        /// </summary>
        /// <param name="service">Сервис выполнения асинхронных задач.</param>
        /// <param name="task">Задача, возвращающая результат (результат игнорируется).</param>
        /// <param name="millisecondsBeforeWait">Кол-во миллисекунд на выполнение задачи. После этого времени принудительный Wait().</param>
        public static void ExecuteWithTimeoutWait<T>(this IAsyncExecutionService service, Func<Task<T>> task, int millisecondsBeforeWait)
        {
            service.ExecuteWithTimeoutWait(async _ => { await task(); }, millisecondsBeforeWait);
        }

        /// <summary>
        /// Запустить выполнение задачи. Спустя заданное время ожидания, если задача не завершилась, принудительная отмена.
        /// </summary>
        /// <param name="service">Сервис выполнения асинхронных задач.</param>
        /// <param name="task">Задача, которую нужно выполнить (без CancellationToken).</param>
        /// <param name="millisecondsBeforeCancel">Кол-во миллисекунд на выполнение задачи. После этого времени принудительная отмена.</param>
        public static void ExecuteWithTimeoutCancel(this IAsyncExecutionService service, Func<Task> task, int millisecondsBeforeCancel)
        {
            service.ExecuteWithTimeoutCancel(_ => task(), millisecondsBeforeCancel);
        }

        /// <summary>
        /// Запустить выполнение задачи. Спустя заданное время ожидания, если задача не завершилась, принудительная отмена.
        /// </summary>
        /// <param name="service">Сервис выполнения асинхронных задач.</param>
        /// <param name="task">Синхронное действие, которое будет выполнено в пуле потоков.</param>
        /// <param name="millisecondsBeforeCancel">Кол-во миллисекунд на выполнение задачи. После этого времени принудительная отмена.</param>
        public static void ExecuteWithTimeoutCancel(this IAsyncExecutionService service, Action action, int millisecondsBeforeCancel)
        {
            service.ExecuteWithTimeoutCancel(ct => Task.Run(action, ct), millisecondsBeforeCancel);
        }

        /// <summary>
        /// Запустить выполнение задачи. Спустя заданное время ожидания, если задача не завершилась, принудительная отмена.
        /// </summary>
        /// <param name="service">Сервис выполнения асинхронных задач.</param>
        /// <param name="task">Задача, возвращающая результат (результат игнорируется).</param>
        /// <param name="millisecondsBeforeCancel">Кол-во миллисекунд на выполнение задачи. После этого времени принудительная отмена.</param>
        public static void ExecuteWithTimeoutCancel<T>(this IAsyncExecutionService service, Func<Task<T>> task, int millisecondsBeforeCancel)
        {
            service.ExecuteWithTimeoutCancel(async ct => { await task(); }, millisecondsBeforeCancel);
        }
    }
}