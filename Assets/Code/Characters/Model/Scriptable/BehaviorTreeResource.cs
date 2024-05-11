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
}