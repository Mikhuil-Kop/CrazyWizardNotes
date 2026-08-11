using UNet;
using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Magic.Realization
{
    /// <summary>
    /// Статистика о состоянии частицы. Поля этого объекта нельзя обновлять через Behavior. Они обновляются автоматически в CustomSpellParticle.
    /// </summary>
    public interface ICustomParticleBehaviorState
    {
        /* ------------------------------------------ Контроль ------------------------------------------ */

        /// <summary>
        /// Контролируется ли частица в данный момент.
        /// </summary>
        public bool IsBending { get; }

        /// <summary>
        /// Сколько раз частицу начинали контролировать с момента её создания.
        /// </summary>
        public int StartBendCount { get; }

        /// <summary>
        /// Сколько раз частицу переставали контролировать с момента её создания.
        /// </summary>
        public int EndBendCount { get; }

        /// <summary>
        /// Точка, в которую магический поток направляет частицу.
        /// Null, если частица не контролируется в данный момент.
        /// </summary>
        public SpacePoint? BendingTargetPoint { get; }

        /* ------------------------------------------ Время ------------------------------------------ */

        /// <summary>
        /// Сколько времени частица существует.
        /// </summary>
        public float SumExistsTime { get; }

        /// <summary>
        /// Сколько времени частица находится в контроле в данный момент.
        /// </summary>
        public float CurBendTime { get; }

        /// <summary>
        /// Сколько времени частица была в контроле за всё время.
        /// </summary>
        public float SumBendTime { get; }

        /// <summary>
        /// Сколько времени частица находится свободной от контроля в данный момент.
        /// </summary>
        public float CurFreeTime { get; }

        /// <summary>
        /// Сколько времени частица была свободна от контроля за всё время.
        /// </summary>
        public float SumFreeTime { get; }
    }
}
