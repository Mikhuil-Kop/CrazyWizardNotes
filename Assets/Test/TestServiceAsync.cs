using UnityEngine;
using System.Threading.Tasks;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using CrazyWizardNotes.Common.Services;

public class TestServiceAsync : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Start");
            AsyncQueueService.Instance.Invoke(DoAsync());
        }
    }

    private async Task DoAsync()
    {
        var tasks = Enumerable
            .Range(0, 100)
            .Select(async x =>
            {
                Debug.Log(x + " Work");
                await Task.Delay(10);
            })
            .ToArray();

        await Task.WhenAll(tasks);
    }
}

