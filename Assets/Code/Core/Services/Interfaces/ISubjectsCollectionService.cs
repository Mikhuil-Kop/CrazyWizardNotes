using System.Collections.Generic;
using CrazyWizardNotes.Interfaces;

namespace CrazyWizardNotes.Common.Services
{
    public interface ISubjectsCollectionService
    {
        void AddSubject(ISubject subject);
        IEnumerable<ISubject> GetAll();
        IEnumerable<T> Get<T>() where T : class;

    }
}