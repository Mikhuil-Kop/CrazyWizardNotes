using System;
using System.Collections.Generic;
using System.Linq;
using DS.Utilities;
using UnityEditor;
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
        private EditorWindow ParentWindow { get; set; }

        private SearchWindow SearchWindow { get; set; }

        protected IEnumerable<N> Nodes => nodes.Select(x => x as N).Where(x => x != null);

        protected IEnumerable<NodeConnection> Connections => edges.Select(x => new NodeConnection()
        {
            NodeFrom = Nodes.First(y => y.outputContainer.Children().Any(p => p is Port && (p as Port).connections.Contains(x))),
            NodeTo = Nodes.First(y => y.inputContainer.Children().Any(p => p is Port && (p as Port).connections.Contains(x))),
            PortFrom = x.output,
            PortTo = x.input
        });

        public GraphViewBase(EditorWindow parent)
        {
            ParentWindow = ParentWindow;

            AddManipulators();
            AddGridBackground();
            AddSearchWindow();
            AddStyles();
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

        protected abstract IEnumerable<ContextButtonInfo> GetContextButtons();

        #region Инициализация

        private void AddManipulators()
        {
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            foreach (var buttonInfo in GetContextButtons())
            {
                var manipulator = new ContextualMenuManipulator(menuEvent =>
                    menuEvent.menu.AppendAction(buttonInfo.Title, buttonInfo.OnClick));

                this.AddManipulator(manipulator);
            }
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
                SearchWindow = ScriptableObject.CreateInstance<SearchWindow>();
            }

            nodeCreationRequest = context => UnityEditor.Experimental.GraphView.SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), SearchWindow);
        }

        private void AddStyles()
        {
            this.AddStyleSheets(
                "DialogueSystem/DSGraphViewStyles.uss",
                "DialogueSystem/DSNodeStyles.uss"
            );
        }

        #endregion

        #region Вспомогательное

        protected Vector2 GetLocalMousePosition(Vector2 mousePosition, bool isSearchWindow = false)
        {
            var worldMousePosition = mousePosition;

            if (isSearchWindow)
            {
                worldMousePosition = ParentWindow.rootVisualElement.ChangeCoordinatesTo(ParentWindow.rootVisualElement.parent, mousePosition - ParentWindow.position.position);
            }

            var localMousePosition = contentViewContainer.WorldToLocal(worldMousePosition);

            return localMousePosition;
        }

        #endregion

        #region Внутренние классы

        protected class ContextButtonInfo
        {
            public string Title { get; set; }
            public Action<DropdownMenuAction> OnClick { get; set; }
        }

        protected class NodeConnection
        {
            public N NodeFrom { get; set; }
            public N NodeTo { get; set; }
            public Port PortFrom { get; set; }
            public Port PortTo { get; set; }
        }

        #endregion
    }
}