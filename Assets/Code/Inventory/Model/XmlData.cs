using System;
using System.Xml.Linq;
using UnityEngine;

namespace CrazyWizardNotes.Inventory
{
    [CreateAssetMenu(fileName = "new XMLData", menuName = "ScriptableObjects/XMLData", order = 1)]
    public class XmlData : ScriptableObject
    {
        [TextArea(1, 50)]
        public string XML;

        public XElement XElement => XElement.Parse(XML);
    }
}