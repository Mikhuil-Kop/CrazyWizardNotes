namespace CrazyWizardNotes.Totems.Abstractions
{
    /// <summary>
    /// Небольшой интерфейс для визуализаторов тотемов. Не включает в себя игровую логику. Является дополнением к <see cref="ITotem"/>.
    /// </summary>
    public interface ITotemVisualizer
    {
        /// <summary>
        /// Вызывается при <see cref="ITotem.Enable()"/>.
        /// </summary>
        void RunEnableAnimation();

        /// <summary>
        /// Вызывается при <see cref="ITotem.Disable()"/>.
        /// </summary>
        void RunDisableAnimation();

        /// <summary>
        /// Вызывается при <see cref="ITotem.Activate()"/>.
        /// </summary>
        void RunActivateAnimation();

        /// <summary>
        /// Вызывается при <see cref="ITotem.ActivateOnce()"/>.
        /// </summary>
        void RunActivateOnceAnimation();

        /// <summary>
        /// Вызывается при <see cref="ITotem.Deactivate()"/>.
        /// </summary>
        void RunDeactivateAnimation();

        /// <summary>
        /// Вызывается при повторной активации эффекта, когда тотем включен и активен постоянно.
        /// </summary>
        void RunActivateRaiseAnimation();
    }
}