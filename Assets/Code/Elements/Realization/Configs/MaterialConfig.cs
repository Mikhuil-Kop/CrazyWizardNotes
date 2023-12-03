using System;

namespace CrazyWizardNotes.Elements.Realization
{
    [Serializable]
    public class MaterialConfig
    {
        public float ThermalCapacity;
        public float ThermalConductivity;
        public float ThermalEmissivity;

        public float MoistureCapacity;
        public float MoistureLimit;

        public float AutoignitionTemperature;
        public float FlashPointTemperature;
        public float Flammability;

        public float ElectricCapacity;
        public float ElectricResistance;
        public float MagneticSusceptibility;

        public bool IsMetalicVisibly;
    }
}
