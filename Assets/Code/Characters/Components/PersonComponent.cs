using CrazyWizardNotes.Environment.Components;

namespace CrazyWizardNotes.Characters
{
    public class PersonComponent : ObjectComponentBase, IIntelligent, ITalking, IThinking
    {
        public string GetCurrentThoughtDialog()
        {
            throw new System.NotImplementedException();
        }
    }
}
