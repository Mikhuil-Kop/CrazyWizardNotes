namespace CrazyWizardNotes.Editor
{
    public class EventCallNode : BehaviorNodeBase
    {
        public EventCallNode(string id = null, string[] param = null) : base("EventCall", id, param ?? new string[2])
        {
        }

        public override void Draw()
        {
            base.Draw();

            this.AddTextField("Event Name", Params[0], (x) => { Params[0] = x.newValue; });
            this.AddTextField("Event Param", Params[1], (x) => { Params[1] = x.newValue; });
        }
    }
}