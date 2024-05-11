namespace CrazyWizardNotes.Editor
{
    public class AnyNode : BehaviorNodeBase
    {
        public AnyNode(string id = null, string[] param = null) : base("Any", id, param ?? new string[2])
        {
        }
    }
}