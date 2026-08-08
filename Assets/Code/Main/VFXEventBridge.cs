using UnityEngine;
using UnityEngine.VFX;

namespace CrazyWizardNotes.Main
{
    public class VFXEventBridge : MonoBehaviour
    {
        public VisualEffect Vfx;

        // Вызывается из Animation Event
        public void SendVFXEvent(string eventName)
        {
            Vfx.SendEvent(eventName);
        }
    }
}
