using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityEngine;
using CrazyWizardNotes.Inventory;
using CrazyWizardNotes.Inventory.OpenedBooks;
using System.Xml.Linq;

namespace CrazyWizardNotes.CustEditor
{

    [CustomEditor(typeof(OpenedBookBase), true)]
    public class InventoryObjectInspector : Editor
    {
        private IInventoryObject Target { get; set; }
        private string TargetXML { get; set; }

        public override VisualElement CreateInspectorGUI()
        {
            return base.CreateInspectorGUI();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Get XML"))
            {
                var obj = target as IInventoryObject;
                var xml = obj.GetInventoryData();

                TargetXML = xml.ToString();
            }

            if (GUILayout.Button("Set XML"))
            {
                var obj = target as IInventoryObject;
                obj.SetInventoryData(XElement.Parse(TargetXML));
            }

            TargetXML = GUILayout.TextArea(TargetXML);
        }
    }
}