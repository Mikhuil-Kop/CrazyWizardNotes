using CrazyWizardNotes.Magic.Realization.Particles;
using System.Collections.Generic;

namespace CrazyWizardNotes.Magic.Realization
{
    public interface IBehaviourSelector
    {
        CustomParticleBehaviourBase GetBehaviour(HashSet<string> words);
    }
}
