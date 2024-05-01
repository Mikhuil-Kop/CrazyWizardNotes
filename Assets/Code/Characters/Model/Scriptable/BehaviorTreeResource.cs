using System;
using UnityEngine;

namespace CrazyWizardNotes.Characters
{
    /// <summary>
    /// Формат для сохранения в файл.
    /// </summary>
    [CreateAssetMenu(fileName = "new BehaviorTreeResource", menuName = "ScriptableObjects/BehaviorTreeResource", order = 1)]
    public class BehaviorTreeResource : ScriptableObject
    {
        public BehaviorTreeNodeResource[] Nodes;
        public BehaviorTreeLinkResource[] Links;
    }

    public class BehaviorTreeNodeResource : ScriptableObject
    {
        public string NodeID;
        public string NodeType;
        public string[] Params;
        public Vector2 Position;
    }

    public class BehaviorTreeLinkResource : ScriptableObject
    {
        public string NodeID1;
        public string NodeID2;
    }
}