using CrazyWizardNotes.Inventory;
using CrazyWizardNotes.Inventory.OpenedBooks;
using UnityEditor;
using UnityEngine;

namespace CrazyWizardNodes.Editor
{
    [CustomEditor(typeof(DefaultOpenedBook), true)]
    public class InventoryObjectInspector : UnityEditor.Editor
    {
        private IInventoryObject InventoryObject { get; set; }
        private Vector3 Vector;

        void OnEnable()
        {
            InventoryObject = target as IInventoryObject;
            Debug.Log("123sdas12312");
        }

        public override void OnInspectorGUI()
        {
            GUILayout.TextArea("1321");
            Debug.Log("12312312");
        }

        //public void OnSceneGUI()
        //{
        //    var t = (target as IInventoryObject);

        //    EditorGUI.BeginChangeCheck();
        //    var pos = Handles.PositionHandle(Vector, Quaternion.identity);

        //    if (EditorGUI.EndChangeCheck())
        //    {
        //        Vector = pos;
        //    }
        //}
    }
}