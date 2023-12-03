using System;

namespace CrazyWizardNotes.Elements.Abstractions
{
    public interface IHasTemperature
    {
        /// <summary>
        /// Текущая температура.
        /// </summary>
        float Temperature { get; set; }

        /// <summary>
        /// Текущая тепловая энергия объекта (Джоули).
        /// </summary>
        float ThermalEnergy { get; set; }

        /// <summary>
        /// Теплоёмкость (m * c).
        /// </summary>
        float ThermalCapacity { get; }

        /// <summary>
        /// Теплопроводность (λ).
        /// </summary>
        float ThermalConductivity { get; }

        /// <summary>
        /// Излучательная способность (ε, 0..1).
        /// </summary>
        float ThermalEmissivity { get; }

        event TemperatureChangedEventHandler OnTemperatureChanged;
    }

    public static class IHasTemperatureExtensions
    {
        public static void SetTemperature(this IHasTemperature hasTemperature, float newTemperature)
        {
            if (hasTemperature == null) throw new ArgumentNullException(nameof(hasTemperature));

            hasTemperature.Temperature = newTemperature;
        }

        public static void SetThermalEnergy(this IHasTemperature hasTemperature, float newThermalEnergy)
        {
            if (hasTemperature == null) throw new ArgumentNullException(nameof(hasTemperature));

            hasTemperature.ThermalEnergy = newThermalEnergy;
        }

        public static void AddTemperature(this IHasTemperature hasTemperature, float deltaTemperature)
        {
            if (hasTemperature == null) throw new ArgumentNullException(nameof(hasTemperature));

            hasTemperature.Temperature += deltaTemperature;
        }

        public static void AddThermalEnergy(this IHasTemperature hasTemperature, float deltaThermalEnergy)
        {
            if (hasTemperature == null) throw new ArgumentNullException(nameof(hasTemperature));

            hasTemperature.ThermalEnergy += deltaThermalEnergy;
        }
    }
}
