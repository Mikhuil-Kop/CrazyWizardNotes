using System;
using UnityEngine;

namespace CrazyWizardNotes.Characters.Abstractions
{
    [Serializable]
    public class BehaviorTreeNodeResource
    {
        public string NodeID;
        public string NodeType;
        public string[] Params;
        public Vector2 Position;
    }

}