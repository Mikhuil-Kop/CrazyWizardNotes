using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CrazyWizardNotes.Characters
{
    public class BehaviorController : MonoBehaviour, IBehaviorController
    {
        #region Поля

        [SerializeField]
        private BehaviorTreeResource BehaviorTreeResource;

        private BehaviorTree Behavior { get; set; }

        private BehaviorTreeNode CurrentNode { get; set; }

        public event Action<string> OnEventCall;

        #endregion

        #region Основной функционал

        public void Awake()
        {
            Behavior = ParseTreeResource(BehaviorTreeResource);
        }

        public void RaiseEvent(string eventName, string param = null)
        {
            if (CurrentNode.Links.TryGetValue(eventName, out var newNode))
            {
                CurrentNode = newNode;
                CurrentNode.OnStart?.Invoke();
            }
        }

        private BehaviorTree ParseTreeResource(BehaviorTreeResource resource)
        {
            var factories = new Dictionary<string, Func<BehaviorTreeNodeResource, BehaviorTreeNode>>()
            {
                {  "Start", x => new BehaviorTreeNode() { Links = new Dictionary<string, BehaviorTreeNode>(), OnStart = () => { } } },
                {  "Any", x => new BehaviorTreeNode() { Links = new Dictionary<string, BehaviorTreeNode>(), OnStart = () => { } } },
                {  "Dialog", x => new BehaviorTreeNode() { Links = new Dictionary<string, BehaviorTreeNode>(), OnStart = () => { } } },
                {  "EventCall", x => new BehaviorTreeNode() { Links = new Dictionary<string, BehaviorTreeNode>(), OnStart = () => { } } }
            };

            var nodes = resource.Nodes.ToDictionary(
                x => x.NodeID,
                x => factories[x.NodeType].Invoke(x));

            foreach (var link in resource.Links)
            {
                var nodeFrom = nodes[link.NodeFromID];
                var nodeTo = nodes[link.NodeToID];

                nodeFrom.Links.Add(link.PortFromParam, nodeTo);
            }

            var tree = new BehaviorTree()
            {
                Root = nodes.Select(x => x.Value).First(x => x != null)
            };

            return tree;
        }

        #endregion

        #region Реализация интерфейса

        event Action<string> IBehaviorController.OnEventCall
        {
            add => OnEventCall += value;
            remove => OnEventCall -= value;
        }

        void IBehaviorController.RaiseEvent(string eventName, string param)
        {
            RaiseEvent(eventName, param);
        }

        #endregion

        #region Классы

        private class BehaviorTree
        {
            public BehaviorTreeNode Root { get; set; }
        }

        private class BehaviorTreeNode
        {
            public Dictionary<string, BehaviorTreeNode> Links { get; set; }
            public Action OnStart { get; set; }
        }

        #endregion
    }

}
