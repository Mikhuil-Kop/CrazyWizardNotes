namespace CrazyWizardNotes.Elements.Abstractions
{
    public interface IMagnetic
    {
        /// <summary>
        /// Влияют ли магниты физически на объект.
        /// </summary>
        bool IsAffectedByMagnetic { get; }

        /// <summary>
        /// Собственный "заряд" магнита. Определяет силу магнитного поля и его полярность.
        /// </summary>
        float MagneticCharge { get; set; }

        /// <summary>
        /// Насколько объект поддается намагничиванию от других магнитов.
        /// </summary>
        float MagneticSusceptibility { get; }
    }
}
