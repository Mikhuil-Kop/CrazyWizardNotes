namespace CrazyWizardNotes.Elements.Abstractions
{
    public interface IHasMoisture
    {
        /// <summary>
        /// Текущая влажность.
        /// </summary>
        float Moisture { get; set; }

        /// <summary>
        /// Текущая влажность в процентах. (Moisture / MoistureCapacity)
        /// </summary>
        float MoisturePercent { get; set; }

        /// <summary>
        /// Вместительность влажности.
        /// </summary>
        float MoistureCapacity { get; }

        event MoistureChangedEventHandler OnMoistureChanged;
    }
}
