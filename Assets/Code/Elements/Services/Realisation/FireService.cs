using CrazyWizardNotes.Common;
using CrazyWizardNotes.Common.Services;
using CrazyWizardNotes.Interfaces;
using System.Linq;
using UnityEngine;

namespace CrazyWizardNotes.Elements
{
    public class FireService : SingletonBehavior<FireService>
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
                    Flammable = x.As<IFlammable>(),
                    ContainsEnergy = x.As<IContainsEnergy>(),
                })
                .Where(x =>
                    x.Flammable != null &&
                    x.ContainsEnergy != null);

            foreach (var obj in objects)
            {
                var flammable = obj.Flammable;
                var containsEnergy = obj.ContainsEnergy;

                if (containsEnergy.ContainedEnergy.Temperature > flammable.EnergyToStartBurning)
                {
                    flammable.Burning = true;
                }

                if (flammable.Burning)
                {
                    var burnout = Mathf.Min(flammable.CurrentFuelEnergy, flammable.FuelEnergyBurnoutSpeed * Time.deltaTime);

                    if (burnout > 0)
                    {
                        flammable.CurrentFuelEnergy -= burnout;
                        containsEnergy.AddEnergy(new ElementsValues() { Temperature = burnout });
                    }
                }
            }
        }
    }
}
