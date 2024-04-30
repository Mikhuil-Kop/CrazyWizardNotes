using System;
using System.Collections.Generic;

namespace CrazyWizardNotes.Spells.Logic
{
    public class SpellContext
    {
        public Spell Spell { get; set; }

        public double DeltaTime { get; set; }

        public double TimeFromStart { get; set; }

        public int Ticks { get; set; }

        public IEnumerable<object> Components { get; set; }
    }
}