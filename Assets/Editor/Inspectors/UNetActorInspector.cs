using UNet.Core.Realization;
using UnityEditor;
using Inspector = UnityEditor.Editor;

namespace CrazyWizardNotes.Editor
{
    [CustomEditor(typeof(UNetActor))]
    public class UNetActorInspector : Inspector
    {
        private UNetActor actor;
        private static bool showProvider = false;
        private static bool showComponents = false;

        private void OnEnable()
        {
            actor = (UNetActor)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            UNetInspectorTools.LayoutProvider(actor.ServiceProvider as UNetServiceProvider, ref showProvider);
            UNetInspectorTools.LayoutList(actor.Components, x => x.GetType().Name, ref showComponents);
        }
    }
}