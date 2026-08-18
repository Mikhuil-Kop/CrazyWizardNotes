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
}
