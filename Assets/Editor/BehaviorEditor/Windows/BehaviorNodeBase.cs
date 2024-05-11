using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace CrazyWizardNotes.Editor
{
    public abstract class BehaviorNodeBase : GraphNodeBase
    {
        public string ID { get; protected set; }
        public string Type { get; protected set; }
        public string[] Params { get; protected set; }

        public IEnumerable<Port> InputPorts => inputContainer.Children().Select(y => y as Port).Where(y => y != null);
        public IEnumerable<Port> OutputPorts => outputContainer.Children().Select(y => y as Port).Where(y => y != null);

        protected BehaviorNodeBase(string type, string id, string[] param)
        {
            ID = id ?? System.Guid.NewGuid().ToString();
            Type = type;
            Params = param;
        }

        public override void Draw()
        {
            AddTitleField(Type);

            AddInputPort("In", Port.Capacity.Multi);
            AddRecursionPort();
        }

        public void Connect(VisualElement parent, BehaviorNodeBase node, string name)
        {
            AddRecursionPort(name);

            var portFrom = OutputPorts.First(x => x.userData as string == name);
            var portTo = node.InputPorts.First();

            var edge = new Edge();
            parent.Add(edge);

            edge.output = portFrom;
            edge.input = portTo;

            portFrom.Connect(edge);
            portTo.Connect(edge);

            edge.MarkDirtyRepaint();
            edge.OnPortChanged(true);
        }

        private void AddRecursionPort(string value = null)
        {
            AddNamedOutputPort(value, x =>
            {
                if (string.IsNullOrWhiteSpace(x.newValue))
                {
                    if (OutputPorts.LastOrDefault() != x.target)
                    {
                        outputContainer.Remove((x.target as VisualElement).parent);
                    }
                }
                else
                {
                    if (OutputPorts.All(y => !string.IsNullOrWhiteSpace(y.userData as string)))
                    {
                        AddRecursionPort();
                    }
                }
            });
        }
    }
}