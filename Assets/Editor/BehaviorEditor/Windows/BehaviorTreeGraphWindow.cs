using System.Collections.Generic;
using CrazyWizardNotes.Characters;
using UnityEditor;

namespace CrazyWizardNotes.Editor
{
    public class BehaviorTreeGraphWindow : GraphWindowBase<BehaviorTreeGraphView, BehaviorNodeBase, BehaviorGroup, BehaviorTreeResource>
    {
        protected override string DefaultFileName => "NewFile";

        [MenuItem("Window/CWN/Behavior Edit")]
        public static void Open()
        {
            GetWindow<BehaviorTreeGraphWindow>("Behavior Edit");
        }

        protected override void Initialize()
        {
        }

        protected override BehaviorTreeGraphView GetGraphView()
        {
            return new BehaviorTreeGraphView(this);
        }

        protected override IEnumerable<ToolbarButtonInfo> GetButtons()
        {
            return new List<ToolbarButtonInfo>()
            {
            };
        }
    }
}