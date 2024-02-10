using UnityEngine;

namespace CrazyWizardNotes.Common.Services
{
    public class CasterService : SingletonBehavior<CasterService>
    {
        [SerializeField]
        private GameObject Caster;

        public Vector2 CasterPosition { get; private set; }


        private void Update()
        {
            CasterPosition = Caster.transform.position;
        }
    }
}