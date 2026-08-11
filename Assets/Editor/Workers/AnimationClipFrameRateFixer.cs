using UnityEditor;
using UnityEngine;

public static class AnimationClipFrameRateFixer
{
    private const float TargetFrameRate = 30f;

    [MenuItem("Tools/Animation/Set Frame Rate to 30 (Selected)")]
    private static void SetSelected()
    {
        foreach (var obj in Selection.objects)
        {
            if (obj is not AnimationClip clip) continue;

            Undo.RecordObject(clip, "Set Frame Rate 30");
            clip.frameRate = TargetFrameRate;
            EditorUtility.SetDirty(clip);
        }

        AssetDatabase.SaveAssets();
    }

    [MenuItem("Tools/Animation/Set Frame Rate to 30 (Selected)", true)]
    private static bool ValidateSelected()
    {
        foreach (var obj in Selection.objects)
            if (obj is AnimationClip) return true;
        return false;
    }
}