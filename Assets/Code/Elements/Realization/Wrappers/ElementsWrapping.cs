using System;
using System.Collections.Generic;

namespace CrazyWizardNotes.Elements.Realization
{
    public static class ElementsWrapping
    {
        private static readonly Type[] ComponentsForPhysicalObject = new Type[]
        {
            typeof(ElectricConductorObject),
            typeof(FlammableObject),
            typeof(MagneticObject),
            typeof(MoistureObject),
            typeof(TemperatureObject),
        };

        private static readonly Type[] WorkersForPhysicalObject = new Type[]
        {
            typeof(FlameRaisesTemperatureWorker),
            typeof(DischargeOnTouchWorker),
            typeof(DischargeRaisesTemperatureWorker),
            typeof(HighTemperatureEvaporates),
        };

        public static IEnumerable<Type> GetComponentsForPhysicalObject() => ComponentsForPhysicalObject;
        public static IEnumerable<Type> GetWorkersForPhysicalObject() => WorkersForPhysicalObject;
    }
}
