using UnityEditor;
using Inspector = UnityEditor.Editor;
using CrazyWizardNotes.Reading.Realization;

namespace CrazyWizardNotes.Editor
{
    [CustomEditor(typeof(ReadTextResource))]
    public class UNetActorInspectora : Inspector
    {
        private ReadTextResource res;

        private void OnEnable()
        {
            res = (ReadTextResource)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            //UNetInspectorTools.LayoutEnum<ReadTextItemResource, ReadTextResourceLanguage>((x) => x.Items);

            serializedObject.ApplyModifiedProperties();
        }
    }
}