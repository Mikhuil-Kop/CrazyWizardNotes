using UNet.Core.Realization;
using UnityEditor;
using Inspector = UnityEditor.Editor;

namespace CrazyWizardNotes.Editor
{
    [CustomEditor(typeof(UNetScene))]
    public class UNetSceneInspector : Inspector
    {
        private UNetScene scene;
        private static bool showProvider = false;

        private void OnEnable()
        {
            scene = (UNetScene)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            UNetInspectorTools.LayoutProvider(scene.GetServiceProvider() as UNetServiceProvider, ref showProvider);
        }
    }
}