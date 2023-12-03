using UnityEditor;
using UnityEditor.Animations;

public static class AnimatorTransitionFixer
{
    [MenuItem("Tools/Animator/Set Exit Time = 1")]
    private static void SetExitTime1()
    {
        foreach (var obj in Selection.objects)
        {
            if (obj is not AnimatorStateTransition transition) continue;

            Undo.RecordObject(transition, "Set Exit Time 1");
            transition.hasExitTime = true;
            transition.exitTime = 1f;
            transition.duration = 0f;
        }
    }

    [MenuItem("Tools/Animator/Set Exit Time = 1", true)]
    private static bool Validate()
    {
        foreach (var obj in Selection.objects)
            if (obj is AnimatorStateTransition) return true;
        return false;
    }
}
