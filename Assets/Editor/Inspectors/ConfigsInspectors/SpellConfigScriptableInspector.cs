using CrazyWizardNotes.Magic.Abstractions;
using UnityEditor;
using Inspector = UnityEditor.Editor;

namespace CrazyWizardNotes.Editor
{
    [CustomEditor(typeof(SpellParamsScriptable), true)]
    public class SpellConfigScriptableInspector : Inspector
    {
        private SpellParamsScriptable res;

        private void OnEnable()
        {
            res = (SpellParamsScriptable)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.TextField("SpellName", res.DefaultSpellName);
            EditorGUILayout.TextField("SpellTitle", res.DefaultSpellTitle);
            EditorGUILayout.TextField("SpellDescription", res.DefaultSpellDescription);
            EditorGUILayout.TextField("SpellParticleAsset", res.DefaultSpellParticleAsset);
            EditorGUILayout.TextField("SpellTotemAsset", res.DefaultSpellTotemAsset);
        }
    }
}