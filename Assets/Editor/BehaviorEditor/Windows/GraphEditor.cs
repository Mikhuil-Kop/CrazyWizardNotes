using System.Collections.Generic;
using CrazyWizardNotes.Characters;
using UnityEngine;

namespace CrazyWizardNotes.Editor
{
    public class BehaviorTreeGraphWindow : GraphWindowBase<BehaviorTreeGraphView, BehaviorNode, BehaviorGroup, BehaviorTreeResource>
    {
        protected override string DefaultFileName => throw new System.NotImplementedException();

        protected override void Initialize()
        {
            throw new System.NotImplementedException();
        }

        protected override BehaviorTreeGraphView GetGraphView()
        {
            return new BehaviorTreeGraphView();
        }

        protected override IEnumerable<ToolbarButtonInfo> GetButtons()
        {
            return new ToolbarButtonInfo[0];
        }
    }

    public class BehaviorTreeGraphView : GraphViewBase<BehaviorNode, BehaviorGroup, BehaviorTreeResource>
    {
        public override BehaviorTreeResource GetResource()
        {
            throw new System.NotImplementedException();
        }

        public override void OpenResource(BehaviorTreeResource resource)
        {
            throw new System.NotImplementedException();
        }

        protected override BehaviorGroup CreateGroup(string title, Vector2 position)
        {
            throw new System.NotImplementedException();
        }

        protected override BehaviorNode CreateNode(string nodeName, Vector2 position)
        {
            throw new System.NotImplementedException();
        }
    }

    public class BehaviorTreeSearchWindow : SearchWindowBase
    {

    }

    public class BehaviorNode : GraphNodeBase
    {
        public override void Draw()
        {
            this.AddTitleField("Title", "Value");
            AddTextField("Title", "Value2");
        }
    }

    public class BehaviorGroup : GraphGroupBase
    {
        public BehaviorGroup(string groupTitle, Vector2 position) : base(groupTitle, position)
        {
        }
    }
}