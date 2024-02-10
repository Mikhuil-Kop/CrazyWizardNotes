using System;
using System.Linq;
using System.Collections.Generic;

namespace CrazyWizardNotes.Spells.Logic.SpellOperands
{
    public class QueryObjectOperand : IQueryObjectsOperand
    {
        public Func<object, bool> Selector { get; set; }

        public Func<IEnumerable<object>> Collection { get; set; }

        public IEnumerable<object> Objects
        {
            get
            {
                return Collection().Where(Selector);
            }
        }

        public QueryObjectOperand(Func<object, bool> selector, Func<IEnumerable<object>> collection)
        {
            Selector = selector;
            Collection = collection;
        }
    }
}