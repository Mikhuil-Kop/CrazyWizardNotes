using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrazyWizardNotes.Test
{
    public class TestThreads : MonoBehaviour
    {
        private Thread thread;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("Start");
                thread?.Abort();
                thread = new Thread(() => Run());
                thread.Start();
            }
        }

        public void OnDestroy()
        {
            thread?.Abort();
        }

        public void Run()
        {
            int i = 0;

            while (i < 1000)
            {
                Debug.Log(i++);
                //transform.Translate(Vector3.up / 1000f);
                Thread.Sleep(100);
            }
        }
    }
}