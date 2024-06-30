using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrazyWizardNotes.Common;
using CrazyWizardNotes.Common.Services;
using CrazyWizardNotes.Core.Model;
using UnityEngine;

namespace CrazyWizardNotes.Core.Services
{
    public class AsyncJobService : SingletonBehavior<IAsyncJobService>, IAsyncJobService
    {
        private IAsyncQueueService AsyncQueueService { get; set; }

        private Stack<JobItem> Jobs { get; set; }

        protected override void Awake()
        {
            base.Awake();

            AsyncQueueService = Locate<IAsyncQueueService>();

            Jobs = new Stack<JobItem>();
        }

        // TEST
        protected void Start()
        {
            var s = 0;

            var job = new AsyncJobParams()
            {
                ReloadTime = 1f,
                AsyncTask = async () =>
                {
                    await Task.Delay(500);
                    s++;
                },
                NormalTask = () =>
                {
                    Debug.Log(s);
                }
            };

            Register(job);
        }

        public void Register(AsyncJobParams job)
        {
            var jobItem = new JobItem()
            {
                JobParams = job,
                RemainingTime = 0
            };

            Jobs.Push(jobItem);
        }

        protected void Update()
        {
            foreach (var job in Jobs)
            {
                job.RemainingTime -= Time.deltaTime;

                if (job.RemainingTime <= 0)
                {
                    AsyncQueueService.Invoke(job.JobParams.AsyncTask.Invoke(), (int)(job.JobParams.ReloadTime * 1000));

                    job.RemainingTime = job.JobParams.ReloadTime;
                }
            }

            foreach (var job in Jobs.Where(x => x.JobParams.NormalTask != null))
            {
                job.JobParams.NormalTask();
            }
        }

        protected void FixedUpdate()
        {
            foreach (var job in Jobs.Where(x => x.JobParams.FixedTask != null))
            {
                job.JobParams.FixedTask();
            }
        }

        private class JobItem
        {
            public AsyncJobParams JobParams { get; set; }
            public float RemainingTime { get; set; }
        }
    }
}