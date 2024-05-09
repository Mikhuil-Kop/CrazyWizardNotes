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

        public void AddRecursionPort()
        {
            AddNamedOutputPort(null, x =>
            {
                if (string.IsNullOrWhiteSpace(x.newValue))
                {
                    if (outputContainer.Children().Select(y => y as Port).Where(y => y != null).LastOrDefault() != x.target)
                    {
                        outputContainer.Remove((x.target as VisualElement).parent);
                    }
                }
                else
                {
                    if (outputContainer.Children().Select(y => y as Port).Where(y => y != null).All(y => !string.IsNullOrWhiteSpace(y.userData as string)))
                    {
                        AddRecursionPort();
                    }
                }
            });
        }
    }
}