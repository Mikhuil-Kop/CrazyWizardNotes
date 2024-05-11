using System;
using UnityEngine;

namespace CrazyWizardNotes.Characters
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