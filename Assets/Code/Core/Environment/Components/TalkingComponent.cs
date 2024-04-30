using UnityEngine;
using CrazyWizardNotes.Interfaces;
using System.Xml.Linq;
using CrazyWizardNotes.Inventory;

namespace CrazyWizardNotes.Environment.Components
{
    public class TalkingComponent : ObjectComponentBase
    {
        private XElement Dialog { get; set; }


        private void Update()
        {
            
        }

        public void StartTalking(string dialogName)
        {
            Dialog = Resources.Load<XmlData>($"Dialogues/{dialogName}").XElement;
        }
    }
}