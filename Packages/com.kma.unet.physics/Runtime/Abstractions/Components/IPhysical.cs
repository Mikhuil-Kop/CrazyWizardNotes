using System;

namespace UNet.Physics.Abstractions
{
    public interface IPhysical
    {
        /// <summary>
        /// Масса объекта, используемая при рассчете сил, действующих на объект.
        /// </summary>
        float PhysicalMass { get; }

        /// <summary>
        /// Масса объекта, используемая при рассчете размера объекта.
        /// </summary>
        float VolumeMass { get; }
        Vector2D ForceSummary { get; }
        Vector2D SpeedSummary { get; }
        void ApplyForce(Vector2D force);
        void ApplyImpulse(Vector2D impulse);
    }

    public static class PhysicalExtensions
    {
        [Obsolete("Сгенерированно ИИ.")]
        public static void ApplyBreak(this IPhysical physical, float force, float deltaTime)
        {
            if(force <= 0f || deltaTime <= 0f)
            {
                return;
            }

            var speedSq = physical.SpeedSummary.MagnitudeSqr;

            if (speedSq < 0.0001f)
            {
                return;
            }

            if (force * deltaTime >= Math.Sqrt(speedSq) * physical.PhysicalMass)
            {
                // Точная остановка ровно в 0
                var stopImpulse = -physical.SpeedSummary * physical.PhysicalMass;
                physical.ApplyImpulse(stopImpulse);
            }
            else
            {
                // Обычное торможение
                var brakingForce = -physical.SpeedSummary.Normalized * force;
                physical.ApplyForce(brakingForce);
            }
        }

        /// <summary>
        /// Применяет импульс к объекту, но с ограничением максимальной скорости.
        /// </summary>
        /// <param name="physical">Физический объект.</param>
        /// <param name="destination">Куда направлено движение. Ненормализованный вектор. Если объект достигает цели, он резко тормозит.</param>
        /// <param name="force">Сила, с которой движется объект.</param>
        /// <param name="maxSpeed">Максимально допустимая скорость после применения импульса.</param>
        /// <param name="deltaTime">Время между кадрами.</param>
        [Obsolete("Сгенерированно ИИ.")]
        public static void ApplyWalk(this IPhysical physical, Vector2D destination, float force, float maxSpeed)
        {
            if (force <= 0f || maxSpeed <= 0f)
            {
                return;
            }

            var currentVelocity = physical.SpeedSummary;
            var toDestination = destination; // уже ненормализованный

            // Если объект уже очень близко к цели — резко тормозим
            if (toDestination.MagnitudeSqr < 0.01f)
            {
                var stopImpulse = -currentVelocity * physical.PhysicalMass;
                physical.ApplyImpulse(stopImpulse);
                return;
            }

            var direction = toDestination.Normalized;

            // Применяем силу в нужном направлении
            var walkForce = direction * force;
            physical.ApplyForce(walkForce);

            // Ограничение максимальной скорости (если превысили — корректируем)
            var currentSpeed = (float)Math.Sqrt(currentVelocity.MagnitudeSqr);
            if (currentSpeed > maxSpeed)
            {
                // Снижаем скорость до maxSpeed, сохраняя направление
                var limitedVelocity = direction * maxSpeed;
                var velocityCorrection = limitedVelocity - currentVelocity;
                var correctionImpulse = velocityCorrection * physical.PhysicalMass;
                physical.ApplyImpulse(correctionImpulse);
            }
        }
    }
}
