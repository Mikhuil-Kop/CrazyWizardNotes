using UnityEditor;
using UnityEditor.Animations;

namespace Editor
{
    public static class AnimatorTransitionDefaults
    {
        [InitializeOnLoadMethod]
        private static void Init()
        {
            ObjectChangeEvents.changesPublished += OnChanges;
        }

        private static void OnChanges(ref ObjectChangeEventStream stream)
        {
            for (var i = 0; i < stream.length; i++)
            {
                if (stream.GetEventType(i) != ObjectChangeKind.CreateAssetObject) continue;

                stream.GetCreateAssetObjectEvent(i, out var data);
                if (EditorUtility.EntityIdToObject(data.entityId) is not AnimatorStateTransition transition) continue;

                transition.hasExitTime = true;
                transition.exitTime = 1f;
                transition.duration = 0f;
            }
        }
    }
}