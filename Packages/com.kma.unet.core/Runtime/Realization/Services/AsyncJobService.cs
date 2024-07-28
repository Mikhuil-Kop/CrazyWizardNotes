using System.Collections.Generic;
using System.Linq;
using UNet.Core.Abstractions;

namespace UNet.Core.Realization
{
    public class AsyncJobService : IAsyncJobService, IHandlesUpdate, IHandlesFixedUpdate
    {
        private IAsyncExecutionService AsyncExecutionService { get; set; }
        private LinkedList<JobItem> AsyncJobs { get; set; }
        private LinkedList<JobItem> NormalJobs { get; set; }
        private LinkedList<JobItem> FixedJobs { get; set; }

        public AsyncJobService(IAsyncExecutionService asyncExecutionService)
        {
            AsyncExecutionService = asyncExecutionService;
            AsyncJobs = new LinkedList<JobItem>();
            NormalJobs = new LinkedList<JobItem>();
            FixedJobs = new LinkedList<JobItem>();
        }

        public void Register(AsyncJobParams job)
        {
            var tasks = new object[] { job.AsyncTask, job.NormalTask, job.FixedTask };
            var tasksCount = tasks.Count(x => x != null);

            if (tasksCount != 1)
            {
                throw new System.ArgumentException("При регистрации AsyncJob необходимо указать ровно один испольняемый метод.", "job");
            }

            if (job.RepeatTime <= 0 && job.RepeatCount != null)
            {
                throw new System.ArgumentException("При регистрации AsyncJob необходимо указать время, спустя которое метод будет выполнен вновь, или количество раз, сколько метод надо будет выполнить.", "job");
            }

            var jobItem = new JobItem()
            {
                JobParams = job,
                RemainingTime = job.InitialDelay,
                RemainingCount = job.RepeatCount
            };

            if (job.FixedTask != null)
            {
                FixedJobs.AddLast(jobItem);
            }
            else if(job.AsyncTask != null)
            {
                AsyncJobs.AddLast(jobItem);
            }
            else
            {
                NormalJobs.AddLast(jobItem);
            }
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var node in AsyncJobs.AsNodes())
            {
                var job = node.Value;
                job.RemainingTime -= deltaTime;

                while (job.RemainingTime <= 0f)
                {
                    job.RemainingTime += job.JobParams.RepeatTime;
                    ExecuteAsyncJob(job);

                    if (job.RemainingCount != null)
                    {
                        job.RemainingCount -= 1;

                        if (job.RemainingCount <= 0)
                        {
                            AsyncJobs.Remove(node);
                        }
                    }
                }
            }

            foreach (var node in NormalJobs.AsNodes())
            {
                var job = node.Value;
                job.RemainingTime -= deltaTime;

                while (job.RemainingTime <= 0f)
                {
                    job.RemainingTime += job.JobParams.RepeatTime;
                    job.JobParams.NormalTask();

                    if (job.RemainingCount != null)
                    {
                        job.RemainingCount -= 1;

                        if (job.RemainingCount <= 0)
                        {
                            NormalJobs.Remove(node);
                        }
                    }
                }
            }
        }

        public void OnFixedUpdate(float deltaTime)
        {
            foreach (var node in FixedJobs.AsNodes())
            {
                var job = node.Value;
                job.RemainingTime -= deltaTime;

                while (job.RemainingTime <= 0f)
                {
                    job.RemainingTime += job.JobParams.RepeatTime;
                    job.JobParams.FixedTask();

                    if (job.RemainingCount != null)
                    {
                        job.RemainingCount -= 1;

                        if (job.RemainingCount <= 0)
                        {
                            FixedJobs.Remove(node);
                        }
                    }
                }
            }
        }

        private void ExecuteAsyncJob(JobItem job)
        {
            switch (job.JobParams.WaitType)
            {
                case AsyncJobWaitType.None:
                    AsyncExecutionService.ExecuteWithTimeoutWait(job.JobParams.AsyncTask, int.MaxValue);
                    break;
                case AsyncJobWaitType.WaitImmediately:
                    AsyncExecutionService.ExecuteWithTimeoutWait(job.JobParams.AsyncTask, 0);
                    break;
                case AsyncJobWaitType.WaitBeforeNext:
                    AsyncExecutionService.ExecuteWithTimeoutWait(job.JobParams.AsyncTask, (int)(job.JobParams.RepeatTime * 1000));
                    break;
                case AsyncJobWaitType.CancelBeforeNext:
                    AsyncExecutionService.ExecuteWithTimeoutCancel(job.JobParams.AsyncTask, (int)(job.JobParams.RepeatTime * 1000));
                    break;
            }
        }

        private class JobItem
        {
            public AsyncJobParams JobParams { get; set; }
            public float RemainingTime { get; set; }
            public int? RemainingCount { get; set; }
        }
    }
}