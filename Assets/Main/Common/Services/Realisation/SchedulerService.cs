using System;
using System.Collections.Generic;
using UnityEngine;

namespace CrazyWizardNotes.Common.Services
{
    public class SchedulerService : SingletonBehavior<ISchedulerService>, ISchedulerService
    {
        private LinkedList<Job> Jobs { get; set; }

        private float Timer { get; set; }

        protected override void Awake()
        {
            base.Awake();

            Jobs = new LinkedList<Job>();
        }

        protected void Update()
        {
            Timer += Time.deltaTime;

            var endNode = GetFirstNode((x) => x.ScheduledTime > Timer);
            var node = Jobs.First;

            while (node != endNode)
            {
                var prevNode = node;
                node = node.Next;

                try
                {
                    prevNode.Value.Action.Invoke();
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }

                Jobs.Remove(prevNode);
            }

            if (Timer > 10000f)
            {
                Timer -= 10000f;

                foreach (var job in Jobs)
                {
                    job.ScheduledTime -= 10000f;
                }
            }
        }

        public void Shedule(float delay, Action action)
        {
            var job = new Job() { ScheduledTime = delay + Timer, Action = action };

            var node = GetFirstNode((x) => x.ScheduledTime > Timer + delay);

            if (node != null)
            {
                Jobs.AddBefore(node, job);
            }
            else
            {
                Jobs.AddLast(job);
            }
        }

        private LinkedListNode<Job> GetFirstNode(Func<Job, bool> func)
        {
            var node = Jobs.First;

            while (node != null && !func(node.Value))
            {
                node = node.Next;
            }

            return node;
        }

        private class Job
        {
            public float ScheduledTime { get; set; }
            public Action Action { get; set; }
        }
    }
}