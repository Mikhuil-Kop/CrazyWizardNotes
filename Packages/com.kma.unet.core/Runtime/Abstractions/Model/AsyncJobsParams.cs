using System;
using System.Threading.Tasks;

namespace UNet.Core.Abstractions
{
    public class AsyncJobParams
    {
        public Func<Task> AsyncTask { get; set; }
        public Action NormalTask { get; set; }
        public Action FixedTask { get; set; }

        public float InitialDelay { get; set; }
        public float RepeatTime { get; set; }
        public int? RepeatCount { get; set; }

        public AsyncJobWaitType WaitType { get; set; }
    }
}