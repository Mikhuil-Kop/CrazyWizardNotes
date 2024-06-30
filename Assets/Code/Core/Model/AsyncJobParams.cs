using System;
using System.Threading.Tasks;

namespace CrazyWizardNotes.Core.Model
{
    public class AsyncJobParams
    {
        public Func<Task> AsyncTask { get; set; }
        public Action NormalTask { get; set; }
        public Action FixedTask { get; set; }

        public float ReloadTime { get; set; }
    }
}