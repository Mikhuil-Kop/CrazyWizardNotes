using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<T> Get<T>() where T : class
        {
            return Collection.SubjectsAs<T>();
        }
    }
}