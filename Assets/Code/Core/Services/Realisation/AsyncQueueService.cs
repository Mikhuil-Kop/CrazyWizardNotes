using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace CrazyWizardNotes.Common.Services
{
    public class AsyncQueueService : SingletonBehavior<IAsyncQueueService>, IAsyncQueueService
    {
        private LinkedList<QueueItem> Tasks { get; set; }
        private int CurrentTime { get; set; }

        protected override void Awake()
        {
            base.Awake();
            Tasks = new LinkedList<QueueItem>();
        }

        public void Invoke(Task task)
        {
            Invoke(task, 0);
        }

        public void Invoke(Task task, int milliseconds)
        {
            lock (Tasks)
            {
                Tasks.AddLast(new QueueItem() { Task = task, Milliseconds = CurrentTime + milliseconds });
            }
        }

        protected void Update()
        {
            CurrentTime += (int)(Time.deltaTime * 1000);

            QueueItem[] items = null;

            lock (Tasks)
            {
                items = Tasks.Where(x => x.Milliseconds <= CurrentTime).ToArray();
            }

            Wait(items.Select(x => x.Task));

            lock (Tasks)
            {
                foreach (var item in items)
                {
                    Tasks.Remove(item);
                }
            }
        }

        private void Wait(IEnumerable<Task> tasks)
        {
            Thread thread = null;

            try
            {
                thread = new Thread(() =>
                {
                    Task.WaitAll(tasks.ToArray());
                });

                while (thread.IsAlive)
                {
                    Thread.Sleep(5);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                thread.Abort();
            }
        }

        private class QueueItem
        {
            public Task Task { get; set; }
            public int Milliseconds { get; set; }
        }
    }
}