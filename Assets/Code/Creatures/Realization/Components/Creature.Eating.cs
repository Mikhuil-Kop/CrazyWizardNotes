using CrazyWizardNotes.Creatures.Abstractions;
using System;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Creatures.Realization
{
    public sealed partial class Creature : ICanEat, IEatable
    {
        #region Поля

        /// <summary>
        /// Кол-во калорий, накопленных существом. Считается для определения бонусной массы, энергии существа и его настроения.
        /// Комфортное кол-во калорий: диапазон между <see cref="CaloriesToSaturation"/> и <see cref="CaloriesToOvereating"/>.
        /// </summary>
        private float CaloriesAccumulated { get; set; }

        /// <summary>
        /// Минимальное кол-во калорий для поддержания жизни. Если <see cref="CaloriesAccumulated"/> оказывается больше <see cref="CaloriesToOvereating"/> - существо страдает от переедания.
        /// </summary>
        private float CaloriesToOvereating => 60_000f;

        /// <summary>
        /// Минимальное кол-во калорий для поддержания жизни. Если <see cref="CaloriesAccumulated"/> оказывается меньше <see cref="CaloriesToSaturation"/> - существо ищет еду.
        /// </summary>
        private float CaloriesToSaturation => 50_000f;

        /// <summary>
        /// Минимальное кол-во калорий для поддержания жизни. Если <see cref="CaloriesAccumulated"/> оказывается меньше <see cref="CaloriesToStarve"/> - существо страдает от голода.
        /// </summary>
        private float CaloriesToStarve => 45_000f;

        /// <summary>
        /// Минимальное кол-во калорий для поддержания жизни. Если <see cref="CaloriesAccumulated"/> оказывается меньше <see cref="CaloriesToDeath"/> - существо умирает.
        /// </summary>
        private float CaloriesToDeath => 10_000f;

        /// <summary>
        /// Кол-во калорий в пустом теле существа. Не учитывается при определении поведения существа, но используется для определения пищевой ценности трупа.
        /// </summary>
        private float CaloriesBaseline => 60_000f;

        /// <summary>
        /// Кол-во калорий существа, которое было "откушено" от тела.
        /// Пока не используется.
        /// </summary>
        private float CaloriesBittenOff { get; set; }

        /// <summary>
        /// Кол-во калорий в 1 килограмме существа.
        /// </summary>
        private float CaloriesInKg => 7000f;

        private IActor EatTarget { get; set; }

        #endregion

        #region Реализация

        float ICanEat.CaloriesPerBite => 500f;
        bool ICanEat.EatingIsFull => CaloriesAccumulated >= CaloriesToOvereating;

        float IEatable.CaloriesMax => CaloriesAccumulated + CaloriesBaseline;
        float IEatable.CaloriesLeft => CaloriesAccumulated + CaloriesBaseline - CaloriesBittenOff;

        private void InitializeEat()
        {
            HasAnimationEvents.OnAnimationEvent += (x) =>
            {
                if (x == "Eat_Completed")
                {
                    CompleteEat();
                }
            };
        }

        private void OnRareUpdateEat(float deltaTime)
        {
            CaloriesAccumulated -= deltaTime * 1000f / 60f;
        }

        public void StartEat(IActor target)
        {
            if (IsBusy)
            {
                return;
            }

            var eatable = target?.As<IEatable>();
            var hasSpacePoint = target?.As<IHasSpacePoint>();

            if (eatable == null || hasSpacePoint == null)
            {
                Logger.LogWarning($"Не удалось начать поедание ({target}): она не обладала IEatable и IHasSpacePoint.");
                return;
            }

            EatTarget = target;

            CreatureVisualizer.StartEatAnimation();
        }

        private void CompleteEat()
        {
            if (EatTarget == null)
            {
                return;
            }

            var target = EatTarget;
            var eatable = target.As<IEatable>();
            var hasSpacePoint = target.As<IHasSpacePoint>();
            
            EatTarget = null;

            if (eatable == null || hasSpacePoint == null)
            {
                Logger.LogWarning($"Не удалось поглотить еду ({target}): она не обладала IEatable и IHasSpacePoint.");
                return;
            }

            if (PhysicsService.GetDistance(HasSpacePoint, hasSpacePoint).Magnitude > 1f)
            {
                Logger.LogWarning($"Не удалось поглотить еду ({target}): она слишком далеко.");
                return;
            }

            CaloriesAccumulated += eatable.EatThis(this);
        }

        public float EatThis(ICanEat digester)
        {
            var left = CaloriesAccumulated + CaloriesBaseline - CaloriesBittenOff;
            var loose = Math.Min(digester.CaloriesPerBite, left);

            CaloriesBittenOff += loose;

            return loose;
        }

        #endregion
    }
}