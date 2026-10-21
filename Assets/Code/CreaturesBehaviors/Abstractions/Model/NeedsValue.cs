namespace CrazyWizardNotes.CreaturesBehaviors.Abstractions
{
    public struct NeedsValue
    {
        /// <summary>
        /// Модификатор.
        /// </summary>
        public float SelfishnessMult { get; set; }

        /// <summary>
        /// Модификатор. Определяет насколько сильно 
        /// </summary>
        public float CollectivityMult { get; set; }

        /// <summary>
        /// Модификатор.
        /// </summary>
        public float TimeDelayMod { get; set; }

        public float Air { get; set; }
        public float Water { get; set; }
        public float Food { get; set; }
        public float Sleep { get; set; }
        public float Temperature { get; set; }
        public float Safety { get; set; }
        public float Communication { get; set; }
    }
}
