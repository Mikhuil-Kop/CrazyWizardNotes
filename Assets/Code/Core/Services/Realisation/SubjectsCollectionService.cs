using System.Collections.Generic;
using CrazyWizardNotes.Interfaces;

namespace CrazyWizardNotes.Common.Services
{
    public class SubjectsCollectionService : SingletonBehavior<ISubjectsCollectionService>, ISubjectsCollectionService
    {
        private LinkedList<ISubject> Collection;

        protected override void Awake()
        {
            base.Awake();
            Collection = new LinkedList<ISubject>();
        }

        public void AddSubject(ISubject subject)
        {
            Collection.AddLast(subject);
        }

        public IEnumerable<ISubject> GetAll()
        {
            return Collection;
        }
    }
}