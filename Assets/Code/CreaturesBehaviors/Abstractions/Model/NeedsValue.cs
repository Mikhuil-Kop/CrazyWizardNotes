namespace CrazyWizardNotes.CreaturesBehaviors.Abstractions
{
    public struct NeedsValue
    {
        /// <summary>
        /// Модификатор. Определяет, насколько сильно данные эффекты применимы к самому существу.
        /// </summary>
        public float SelfishnessMult { get; set; }

        /// <summary>
        /// Модификатор. Определяет, насколько сильно данные эффекты применимы к прочим существам.
        /// </summary>
        public float CollectivityMult { get; set; }

        /// <summary>
        /// Модификатор. Определяет, спустя сколько времени эффекты окажут влияние.
        /// </summary>
        public float TimeDelayMod { get; set; }

        // Физиология

        public float Air { get; set; }
        public float Water { get; set; }
        public float Food { get; set; }
        public float Sleep { get; set; }
        public float Temperature { get; set; }
        public float Pleasure { get; set; }

        // Безопасность

        public float Safety { get; set; }

        // Социум

        public float Communication { get; set; }
        public float Love { get; set; }

        // Обучение

        public float Learning { get; set; }

        // Созидание

        public float Creativity { get; set; }
        public float Aesthetics { get; set; }
        public float Nature { get; set; }
    }
}
