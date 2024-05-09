using UnityEngine;

namespace CrazyWizardNotes.Editor
{
    public class DialogNode : BehaviorNodeBase
    {
        public DialogNode(string id = null, string[] param = null) : base("Dialog", id, param ?? new string[1])
        {
        }

        public override void Draw()
        {
            base.Draw();

            this.AddFoldoutTextArea("Text", Params[0], (x) => { Params[0] = x.newValue; });
        }
    }
}