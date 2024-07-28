using System;

namespace CrazyWizardNotes.Characters.Abstractions
{
    [Serializable]
    public class BehaviorTreeLinkResource
    {
        public string NodeFromID;
        public string NodeToID;
        public string PortFromParam;
    }

}