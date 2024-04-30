using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.Experimental;
using UnityEditor.Experimental.GraphView;

namespace CrazyWizardNotes.CustEditor
{
    public class BehaviorEditorView : EditorWindow
    {
        [MenuItem("Window/My/as")]
        public static void Open()
        {
            GetWindow<BehaviorEditorView>("Behavior Editor");
        }

        private void OnEnable()
        {
            AddGraphView();
        }

        private void AddGraphView()
        {
            var graph = new BehaviorGraphView();
            graph.StretchToParentSize();

            rootVisualElement.Add(graph);
        }
    }

    public class BehaviorGraphView : GraphView
    {
        public BehaviorGraphView()
        {
            var background = new GridBackground();
            background.StretchToParentSize();
            Insert(0, background);
        }
    }
}
