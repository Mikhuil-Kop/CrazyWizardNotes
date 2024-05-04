using System.Collections.Generic;

namespace CrazyWizardNotes.Editor
{
    public class A : GraphWindowBase<B>
    {
        protected override string DefaultFileName => throw new System.NotImplementedException();

        protected override void Initialize()
        {
            throw new System.NotImplementedException();
        }

        protected override B GetGraphView()
        {
            return new B();
        }

        protected override IEnumerable<ToolbarButtonInfo> GetButtons()
        {
            throw new System.NotImplementedException();
        }
    }

    public class B : GraphViewBase
    {

    }

    public class C : SearchWindowBase
    {

    }
}