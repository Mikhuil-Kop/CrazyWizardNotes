namespace CrazyWizardNotes.Editor
{
    public class StartNode : BehaviorNodeBase
    {
        public StartNode(string id = null, string[] param = null) : base("Start", id, param ?? new string[2])
        {
        }
    }
}