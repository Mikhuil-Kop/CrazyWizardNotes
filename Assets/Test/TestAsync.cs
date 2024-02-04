using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrazyWizardNotes.Test
{
    public class TestAsync : MonoBehaviour
    {
        private CancellationTokenSource source;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("Start");
                source?.Cancel();
                source = new CancellationTokenSource();
                Task.Run(() => RunAsync(source.Token));
            }
        }

        public void OnDestroy()
        {
            source?.Cancel();
        }

        public async void RunAsync(CancellationToken token)
        {
            int i = 0;

            while (token.IsCancellationRequested)
            {
                Debug.Log(i++);
                transform.Translate(Vector3.up / 1000f);
                await Task.Yield();
                //await Task.Delay(500);
            }
        }
    }
}