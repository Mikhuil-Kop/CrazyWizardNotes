using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using DS.Utilities;

namespace CrazyWizardNotes.Editor
{
    public abstract class GraphWindowBase<T> : EditorWindow where T : GraphViewBase
    {
        protected T GraphView { get; set; }

        protected abstract string DefaultFileName { get; }


        protected abstract void Initialize();

        protected abstract T GetGraphView();

        protected abstract IEnumerable<ToolbarButtonInfo> GetButtons();


        private void OnEnable()
        {
            Initialize();
            AddGraphView();
            AddToolbar();
            AddStyles();
        }

        private void AddGraphView()
        {
            GraphView = GetGraphView();
            GraphView.StretchToParentSize();
            rootVisualElement.Add(GraphView);
        }

        private void AddToolbar()
        {
            var toolbar = new Toolbar();
            toolbar.AddStyleSheets("DialogueSystem/DSToolbarStyles.uss");

            var buttons = GetButtons();

            foreach (var buttonInfo in buttons)
            {
                var button = DSElementUtility.CreateButton(buttonInfo.Title, buttonInfo.OnClick);
                toolbar.Add(button);
            }

            rootVisualElement.Add(toolbar);
        }


        private void AddStyles()
        {
            rootVisualElement.AddStyleSheets("DialogueSystem/DSVariables.uss");
        }

        private void Save()
        {
            //if (string.IsNullOrEmpty(fileNameTextField.value))
            //{
            //    EditorUtility.DisplayDialog("Invalid file name.", "Please ensure the file name you've typed in is valid.", "Roger!");

            //    return;
            //}

            //DSIOUtility.Initialize(graphView, fileNameTextField.value);
            //DSIOUtility.Save();
        }

        private void Load()
        {
            //string filePath = EditorUtility.OpenFilePanel("Dialogue Graphs", "Assets/Editor/DialogueSystem/Graphs", "asset");

            //if (string.IsNullOrEmpty(filePath))
            //{
            //    return;
            //}

            //Clear();

            //DSIOUtility.Initialize(graphView, Path.GetFileNameWithoutExtension(filePath));
            //DSIOUtility.Load();
        }


        protected class ToolbarButtonInfo
        {
            public string Title { get; set; }
            public Action OnClick { get; set; }
        }
    }
}