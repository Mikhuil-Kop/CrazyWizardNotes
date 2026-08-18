namespace CrazyWizardNotes.Elements.Abstractions
{
    public interface IFlammable
    {
        /// <summary>
        /// Температура самовоспламенения (без искры).
        /// </summary>
        float AutoignitionTemperature { get; }

        /// <summary>
        /// Температура вспышки (минимальная для поджига искрой).
        /// </summary>
        float FlashPointTemperature { get; }

        /// <summary>
        /// Горючесть (0..1).
        /// </summary>
        float Flammability { get; }

        /// <summary>
        /// Горит ли объект сейчас.
        /// </summary>
        bool IsBurning { get; }

        /// <summary>
        /// Применить искру / источник зажигания.
        /// </summary>
        void ApplySpark(float intensity);

        /// <summary>
        /// Принудительно зажечь предмет.
        /// </summary>
        void StartBurning();

        /// <summary>
        /// Принудительно потушить предмет.
        /// </summary>
        void EndBurning();

        event IgnitedEventHandler OnIgnited;
        event ExtinguishedEventHandler OnExtinguished;
    }
}
