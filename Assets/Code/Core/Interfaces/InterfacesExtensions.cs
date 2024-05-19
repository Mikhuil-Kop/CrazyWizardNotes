using System;
using System.Linq;
using System.Collections.Generic;
using CrazyWizardNotes.Interfaces;

namespace CrazyWizardNotes.Interfaces
{
    public static class InterfacesExtensions
    {
        public static IEnumerable<T> SubjectsAs<T>(this IEnumerable<ISubject> subjects) where T : class
        {
            return subjects.Select(x => x?.As<T>()).Where(x => x != null);
        }
    }
}