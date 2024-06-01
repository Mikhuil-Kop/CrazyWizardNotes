using CrazyWizardNotes.Common;
using CrazyWizardNotes.Common.Services;
using CrazyWizardNotes.Interfaces;
using System.Linq;
using UnityEngine;

namespace CrazyWizardNotes.Elements
{
    public class EnergyService : SingletonBehavior<EnergyService>
    {
        private ISubjectsCollectionService SubjectsCollectionService { get; set; }

        protected override void Awake()
        {
            base.Awake();

            SubjectsCollectionService = Locate<ISubjectsCollectionService>();
        }

        protected void Update()
        {
            var objects = SubjectsCollectionService
                .GetAll()
                .Select(x => new
                {
                    ContainsEnergy = x.As<IContainsEnergy>(),
                    Touches = x.As<ITouches>(),
                })
                .Where(x =>
                    x.ContainsEnergy != null &&
                    x.Touches != null);

            foreach (var obj in objects)
            {
                var containsEnergy = obj.ContainsEnergy;
                var touches = obj.Touches;

                var connected = touches.TouchingSubjects.SubjectsAs<IContainsEnergy>();

                foreach (var connectedEnergy in connected)
                {
                    var energyDelta = Time.deltaTime * connectedEnergy.EnergyTransfer * containsEnergy.EnergyTransfer * (connectedEnergy.ContainedEnergy - containsEnergy.ContainedEnergy);

                    containsEnergy.AddEnergy(energyDelta);
                    connectedEnergy.AddEnergy(-1 * energyDelta);
                }
            }
        }
    }

}
