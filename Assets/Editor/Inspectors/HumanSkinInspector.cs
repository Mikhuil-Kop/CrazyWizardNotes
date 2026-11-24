using CrazyWizardNotes.Creatures.Realization;
using CrazyWizardNotes.Editor;
using UnityEditor;
using UnityEngine;
using Inspector = UnityEditor.Editor;

[CustomEditor(typeof(HumanSkin))]
public class HumanSkinInspector : Inspector
{
    bool showHeads;
    bool showLeftEyes;
    bool showRightEyes;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.LabelField("Custom");

        var skin = (HumanSkin)target;
        skin.HeadSprites ??= new();
        skin.LeftEyeSprites ??= new();
        skin.RightEyeSprites ??= new();

        UNetInspectorTools.SpritesTableReadonly("Heads", skin.HeadSprites, _ => { },
            h => h.sprite,
            h => h.width,
            h => h.height,
            ref showHeads);

        UNetInspectorTools.SpritesTableReadonly("Left Eyes", skin.LeftEyeSprites, _ => { },
            h => h.sprite,
            h => h.mood,
            h => h.height,
            ref showLeftEyes);

        UNetInspectorTools.SpritesTableReadonly("Right Eyes", skin.RightEyeSprites, _ => { },
            h => h.sprite,
            h => h.mood,
            h => h.height,
            ref showRightEyes);

        if (GUI.changed)
            EditorUtility.SetDirty(skin);
    }
}