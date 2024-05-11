using System;

namespace CrazyWizardNotes.Characters
{
    [Serializable]
    public class BehaviorTreeLinkResource
    {
        public string NodeFromID;
        public string NodeToID;
        public string PortFromParam;
    }

}