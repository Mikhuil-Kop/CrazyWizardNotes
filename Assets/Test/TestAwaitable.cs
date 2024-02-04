using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System;
using UnityEngine;

namespace CrazyWizardNotes.Test
{
    public class TestAwaitable : MonoBehaviour
    {
        private int i;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Debug.Log("Начало" + i++);
                RunProcess(() => WorkAsync());
            }
        }

        public void RunProcess(Func<Task> task)
        {
            Thread thread = null;

            try
            {
                thread = new Thread(() => task().Wait());
                thread.Start();

                while (thread.IsAlive)
                {
                    Thread.Sleep(10);
                }
            }
            catch
            {
                thread?.Abort();
            }
        }

        public async Task WorkAsync()
        {
            Debug.Log("Процесс начат");
            Thread.Sleep(1000);

            for (int x = 0; x < 10; x++)
            {
                var tasks = Enumerable
                    .Range(0, 10)
                    .Select(y => HardWorkAsync(x))
                    .ToArray();

                await Task.WhenAll(tasks);

                Debug.Log(x);
            }
        }

        public async Task HardWorkAsync(int i)
        {
            await Task.Delay(1000);
            Debug.Log("Finished: " + i);
        }
    }
}