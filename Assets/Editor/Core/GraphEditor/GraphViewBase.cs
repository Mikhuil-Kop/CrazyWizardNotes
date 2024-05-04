using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace CrazyWizardNotes.Editor
{
    public abstract class GraphViewBase<N, G, R> : GraphView
        where N : GraphNodeBase
        where G : GraphGroupBase
        where R : ScriptableObject
    {
        private SearchWindowBase SearchWindow { get; set; }

        private MiniMap MiniMap { get; set; }


        public GraphViewBase()
        {
            AddManipulators();
            AddGridBackground();
            AddSearchWindow();
            AddMiniMap();

            AddStyles();
            AddMiniMapStyles();
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports.Where(port =>
            {
                if (startPort == port)
                {
                    return false;
                }

                if (startPort.node == port.node)
                {
                    return false;
                }

                if (startPort.direction == port.direction)
                {
                    return false;
                }

                return true;
            })
            .ToList();
        }

        public abstract void OpenResource(R resource);

        public abstract R GetResource();

        protected abstract G CreateGroup(string title, Vector2 position);

        protected abstract N CreateNode(string nodeName, Vector2 position);


        private void AddManipulators()
        {
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            this.AddManipulator(CreateNodeContextualMenu("Add Node (Single Choice)"));
            this.AddManipulator(CreateNodeContextualMenu("Add Node (Multiple Choice)"));

            this.AddManipulator(CreateGroupContextualMenu());
        }

        private IManipulator CreateNodeContextualMenu(string actionTitle)
        {
            var manipulator = new ContextualMenuManipulator(
                menuEvent => menuEvent.menu.AppendAction(actionTitle, actionEvent => AddElement(CreateNode("DialogueName", dialogueType, GetLocalMousePosition(actionEvent.eventInfo.localMousePosition))))
            );

            return manipulator;
        }

        private IManipulator CreateGroupContextualMenu()
        {
            ContextualMenuManipulator contextualMenuManipulator = new ContextualMenuManipulator(
                menuEvent => menuEvent.menu.AppendAction("Add Group", actionEvent => CreateGroup("DialogueGroup", GetLocalMousePosition(actionEvent.eventInfo.localMousePosition)))
            );

            return contextualMenuManipulator;
        }

        private void AddGridBackground()
        {
            var gridBackground = new GridBackground();

            gridBackground.StretchToParentSize();

            Insert(0, gridBackground);
        }

        private void AddSearchWindow()
        {
            if (SearchWindow == null)
            {
                SearchWindow = ScriptableObject.CreateInstance<SearchWindowBase>();
            }

            SearchWindow.Initialize(this);

            nodeCreationRequest = context => UnityEditor.Experimental.GraphView.SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), SearchWindow);
        }

        private void AddMiniMap()
        {
            MiniMap = new MiniMap()
            {
                anchored = true
            };

            MiniMap.SetPosition(new Rect(15, 50, 200, 180));

            Add(MiniMap);

            MiniMap.visible = false;
        }

        private void AddStyles()
        {
            this.AddStyleSheets(
                "DialogueSystem/DSGraphViewStyles.uss",
                "DialogueSystem/DSNodeStyles.uss"
            );
        }

        private void AddMiniMapStyles()
        {
            StyleColor backgroundColor = new StyleColor(new Color32(29, 29, 30, 255));
            StyleColor borderColor = new StyleColor(new Color32(51, 51, 51, 255));

            MiniMap.style.backgroundColor = backgroundColor;
            MiniMap.style.borderTopColor = borderColor;
            MiniMap.style.borderRightColor = borderColor;
            MiniMap.style.borderBottomColor = borderColor;
            MiniMap.style.borderLeftColor = borderColor;
        }

        public Vector2 GetLocalMousePosition(Vector2 mousePosition, bool isSearchWindow = false)
        {
            Vector2 worldMousePosition = mousePosition;

            if (isSearchWindow)
            {
                worldMousePosition = editorWindow.rootVisualElement.ChangeCoordinatesTo(editorWindow.rootVisualElement.parent, mousePosition - editorWindow.position.position);
            }

            Vector2 localMousePosition = contentViewContainer.WorldToLocal(worldMousePosition);

            return localMousePosition;
        }

        public void ClearGraph()
        {
            graphElements.ForEach(graphElement => RemoveElement(graphElement));

            groups.Clear();
            groupedNodes.Clear();
            ungroupedNodes.Clear();

            NameErrorsAmount = 0;
        }

        public void ToggleMiniMap()
        {
            MiniMap.visible = !MiniMap.visible;
        }
    }
}