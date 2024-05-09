using System;
using System.Linq;
using System.Collections.Generic;
using DS.Utilities;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace CrazyWizardNotes.Editor
{
    public abstract class GraphWindowBase<T, N, G, R> : EditorWindow
        where T : GraphViewBase<N, G, R>
        where N : GraphNodeBase
        where G : GraphGroupBase
        where R : ScriptableObject
    {
        protected T GraphView { get; set; }

        protected abstract string DefaultFileName { get; }

        private string FilePath { get; set; }


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

            var defaultButtons = new List<ToolbarButtonInfo>
            {
                new ToolbarButtonInfo(){ Title = "Load", OnClick = Load },
                new ToolbarButtonInfo(){ Title = "Save", OnClick = Save },
                new ToolbarButtonInfo(){ Title = "Save as", OnClick = SaveAs },
            };

            var addButtons = GetButtons();

            foreach (var buttonInfo in defaultButtons.Concat(addButtons))
            {
                var button = new Button(buttonInfo.OnClick);
                button.text = buttonInfo.Title;

                toolbar.Add(button);
            }

            rootVisualElement.Add(toolbar);
        }

        private void AddStyles()
        {
            rootVisualElement.AddStyleSheets("DialogueSystem/DSVariables.uss");
        }

        #region Сохранение и Загрузка

        private void Load()
        {
            var newfilePath = EditorUtility.OpenFilePanel("Dialogue Graphs", "Assets/Resources", "asset");

            if (string.IsNullOrEmpty(newfilePath))
            {
                return;
            }

            FilePath = "Assets" + newfilePath.Split("Assets")[1];

            var loadPath = FilePath.Split("Resources/")[1].Split(".asset")[0];
            var resource = Resources.Load<R>(loadPath);
            GraphView.OpenResource(resource);
        }

        private void Save()
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                var newfilePath = EditorUtility.SaveFilePanel("Dialogue Graphs", "Assets/Resources", DefaultFileName, "asset");

                if (string.IsNullOrEmpty(newfilePath))
                {
                    return;
                }

                FilePath = "Assets" + newfilePath.Split("Assets")[1];
            }

            var resource = GraphView.GetResource();

            AssetDatabase.CreateAsset(resource, FilePath);
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = resource;
        }

        private void SaveAs()
        {
            var newfilePath = EditorUtility.SaveFilePanel("Dialogue Graphs", "Assets/Resources", DefaultFileName, "asset");

            if (string.IsNullOrEmpty(newfilePath))
            {
                return;
            }

            newfilePath = "Assets" + newfilePath.Split("Assets")[1];

            var resource = GraphView.GetResource();

            AssetDatabase.CreateAsset(resource, newfilePath);
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = resource;
        }

        #endregion

        #region Внутренние классы

        protected class ToolbarButtonInfo
        {
            public string Title { get; set; }
            public Action OnClick { get; set; }
        }

        #endregion
    }
}