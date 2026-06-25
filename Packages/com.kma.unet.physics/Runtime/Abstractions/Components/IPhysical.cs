using System;

namespace UNet.Physics.Abstractions
{
    public interface IPhysical
    {
        float Mass { get; }
        Vector2D ForceSummary { get; }
        Vector2D SpeedSummary { get; }
        void ApplyForce(Vector2D force);
        void ApplyImpulse(Vector2D impulse);
    }

    public static class PhysicalExtensions
    {
        /// <summary>
        /// Применяет импульс к объекту, но с ограничением максимальной скорости.
        /// </summary>
        /// <param name="physical">Физический объект</param>
        /// <param name="impulse">Прикладываемый импульс</param>
        /// <param name="maxSpeed">Максимально допустимая скорость после применения импульса</param>
        [Obsolete("Сгенерированно ИИ.")]
        public static void ApplyImpulseWithLimit(this IPhysical physical, Vector2D impulse, float maxSpeed)
        {
            if (physical == null)
                throw new ArgumentNullException(nameof(physical));

            if (maxSpeed <= 0)
                throw new ArgumentException("Максимальная скорость должна быть больше нуля", nameof(maxSpeed));

            // Сохраняем текущую скорость
            var currentSpeed = physical.SpeedSummary;

            // Вычисляем новую скорость без ограничения
            var newSpeed = currentSpeed + (impulse / physical.Mass);

            // Проверяем, не превышает ли новая скорость лимит
            var newSpeedMagnitude = newSpeed.Magnitude;

            if (newSpeedMagnitude > maxSpeed)
            {
                // Ограничиваем скорость, сохраняя направление
                var limitedSpeed = newSpeed.Normalized * maxSpeed;

                // Пересчитываем импульс, который нужен для достижения ограниченной скорости
                var limitedImpulse = (limitedSpeed - currentSpeed) * physical.Mass;

                // Применяем ограниченный импульс
                physical.ApplyImpulse(limitedImpulse);
            }
            else
            {
                // Импульс в пределах лимита - применяем как есть
                physical.ApplyImpulse(impulse);
            }
        }
    }
}
