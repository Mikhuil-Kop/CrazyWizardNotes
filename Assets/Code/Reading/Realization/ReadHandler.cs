using CrazyWizardNotes.Reading.Abstractions;
using System.Linq;

namespace CrazyWizardNotes.Reading.Realization
{
    public class ReadHandler : IReadHandler
    {
        public string GetKnownText(ReadText text)
        {
            return string.Join("\n", text.Items.Select(x => x.Text));
        }
    }
}
