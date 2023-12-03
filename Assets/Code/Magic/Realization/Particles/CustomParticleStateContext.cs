using UNet.Core.Abstractions;

namespace CrazyWizardNotes.Magic.Realization
{
    /// <summary>
    /// Базовая информация о состоянии частицы. Поля в контексте можно менять для изменения поведения частицы.
    /// </summary>
    public interface ICustomParticleBehaviorContext
    {
        public IActor ParticleActor { get; }
        public ICustomParticleBehaviorState State { get; }
        public float DeltaTime { get; }

        /// <summary>
        /// Эффективность, с которой.
        /// </summary>
        public float BendForceMult { get; set; }
        
        public bool IsBendEnabled { get; set; }

        public bool IsVanishRequested { get; set; }
    }
}
