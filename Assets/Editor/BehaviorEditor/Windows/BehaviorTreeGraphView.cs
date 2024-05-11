using System;
using System.Collections.Generic;
using System.Linq;
using CrazyWizardNotes.Characters;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CrazyWizardNotes.Editor
{
    public class BehaviorTreeGraphView : GraphViewBase<BehaviorNodeBase, BehaviorGroup, BehaviorTreeResource>
    {
        public BehaviorTreeGraphView(EditorWindow parent) : base(parent)
        {
        }

        protected override IEnumerable<ContextButtonInfo> GetContextButtons()
        {
            return new ContextButtonInfo[]
            {
                new ContextButtonInfo(){ Title = "Create Start", OnClick = f => AddNode(new StartNode(), GetLocalMousePosition(f.eventInfo.mousePosition)) },
                new ContextButtonInfo(){ Title = "Create Any", OnClick = f => AddNode(new AnyNode(), GetLocalMousePosition(f.eventInfo.mousePosition)) },
                new ContextButtonInfo(){ Title = "Create Dialog", OnClick = f => AddNode(new DialogNode(), GetLocalMousePosition(f.eventInfo.mousePosition)) },
                new ContextButtonInfo(){ Title = "Create Event Call", OnClick = f => AddNode(new EventCallNode(), GetLocalMousePosition(f.eventInfo.mousePosition)) },
            };
        }

        #region Сохранение и Загрузка

        public override void OpenResource(BehaviorTreeResource resource)
        {
            var factory = new Dictionary<string, Func<BehaviorTreeNodeResource, BehaviorNodeBase>>
            {
                { "Start", (x) => new StartNode(x.NodeID, x.Params) },
                { "Any", (x) => new AnyNode(x.NodeID, x.Params) },
                { "Dialog", (x) => new DialogNode(x.NodeID, x.Params) },
                { "EventCall", (x) => new EventCallNode(x.NodeID, x.Params) },
            };

            foreach (var node in nodes)
            {
                node.parent.Remove(node);
            }

            foreach (var edge in edges)
            {
                edge.parent.Remove(edge);
            }

            foreach (var node in resource.Nodes)
            {
                AddNode(factory[node.NodeType].Invoke(node), node.Position);
            }

            foreach (var link in resource.Links)
            {
                var nodeFrom = Nodes.First(x => x.ID == link.NodeFromID);
                var nodeTo = Nodes.First(x => x.ID == link.NodeToID);

                nodeFrom.Connect(contentContainer, nodeTo, link.PortFromParam);
            }
        }

        public override BehaviorTreeResource GetResource()
        {
            var tree = ScriptableObject.CreateInstance<BehaviorTreeResource>();

            tree.Nodes = Nodes
                .Select(x => new BehaviorTreeNodeResource()
                {
                    NodeID = x.ID,
                    NodeType = x.Type,
                    Params = x.Params,
                    Position = x.GetPosition().position
                })
                .ToArray();

            tree.Links = Connections
                .Select(x => new BehaviorTreeLinkResource()
                {
                    NodeFromID = x.NodeFrom.ID,
                    NodeToID = x.NodeTo.ID,
                    PortFromParam = x.PortFrom.userData as string
                })
                .ToArray();

            return tree;
        }

        #endregion

        #region Функционал

        public void AddNode(BehaviorNodeBase node, Vector2 position)
        {
            AddElement(node);

            node.SetPosition(new Rect() { position = position, size = Vector2.one });
            node.Draw();
        }

        #endregion
    }
}