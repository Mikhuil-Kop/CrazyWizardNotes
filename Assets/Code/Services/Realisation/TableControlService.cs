using UnityEngine;

namespace CrazyWizardNotes.Common.Services
{
    public class TableControlService : SingletonBehavior<TableControlService>
    {


        public void Update()
        {
            var inputService = InputService.Instance;

            if (inputService.GetButtonDown("mouse"))
            {
                Debug.Log("WizardControlService - StartCasting");

            }

            if (inputService.GetButton("mouse"))
            {
                Debug.Log("WizardControlService - TickCasting");

            }

            if (inputService.GetButtonUp("mouse"))
            {
                Debug.Log("WizardControlService - EndCasting");

            }
        }
    }
}