using System;

namespace CrazyWizardNotes.Totems.Abstractions
{
    /// <summary>
    /// Тотемы - особые структуры в мире игры. Они активируются при определенных условиях.
    /// </summary>
    public interface ITotem
    {
        /// <summary>
        /// Эффект, который будет применен при активации тотема.
        /// Эффект срабатывает при активации тотема и повторяется с периодом <see cref="ActivationTimeGap"/> до тех пор, пока тотем активен.
        /// </summary>
        event TotemActivationHandler OnActivated;

        /// <summary>
        /// Эффект, который будет применен при включении тотема.
        /// </summary>
        event TotemEnabledHandler OnEnabled;

        /// <summary>
        /// Задержка между вызовом <see cref="OnActivated"/>, если
        /// </summary>
        TimeSpan ActivationTimeGap { get; set; }

        /// <summary>
        /// Включен ли тотем. Тотем может быть активирован только если он включен.
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// Активен ли тотем.
        /// </summary>
        bool IsActive { get; set; }

        /// <summary>
        /// Установить задержку между вызовом <see cref="OnActivated"/>. Если тотем активен, то срабатывание <see cref="OnActivated"/> будет происходить с новой задержкой.
        /// </summary>
        void SetActivationTimeGap(TimeSpan timeGap);

        /// <summary>
        /// Включить или выключить тотем. Тотем может быть активирован только если он включен.
        /// </summary>
        void SetEnabled(bool enabled);

        /// <summary>
        /// Включает тотем. Тотем может быть активирован только если он включен.
        /// </summary>
        void Enable();

        /// <summary>
        /// Выключает тотем. Тотем не может быть активирован, если он выключен.
        /// </summary>
        void Disable();

        /// <summary>
        /// Активировать или деактивировать тотем.
        /// </summary>
        void SetActive(bool active);

        /// <summary>
        /// Активирует тотем один раз и сразу деактивирует. Вызовет одно срабатывание <see cref="OnActivated"/>.
        /// </summary>
        void ActivateOnce();

        /// <summary>
        /// Активирует тотем. Вызовет одно срабатывание <see cref="OnActivated"/> сразу и еще 1 спустя каждые <see cref="ActivationTimeGap"/>.
        /// </summary>
        void Activate();

        /// <summary>
        /// Деактивирует тотем.
        /// </summary>
        void Deactivate();
    }
}
