using System;
using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Creatures.Abstractions
{
    /// <summary>
    /// Интерфейс, определяющий существо в игре. Не включает в себя разум и поведение этого существа.
    /// </summary>
    public interface ICreature
    {

    }

    public interface ICanWalk
    {
        float AverageWalkSpeed { get; }
        public void WalkTo(SpacePoint spacePoint);
    }

    public interface ICanCarry
    {
        public void Carry(ICarrieble carry);
    }

    public interface ICarrieble
    {

    }

    public interface ICanAttack
    {
        [Obsolete("Еще не готово.")]
        public void Attack();
    }

    public interface IHasName
    {
        public string Name { get; }
    }

    public interface IHasEmotion
    {
        public Emotion Emotion { get; }
    }

    public enum Emotion : byte
    {
        Neutral = 0,
        Happy = 1,
        Sad = 2,
        Cries = 3,
        Angry = 4,
        Feared = 5,
        Surprised = 6,
        Disgusted = 7,
        Aroused = 8
    }

    public interface IClaimable
    {
        public bool TryClaim(string group, object owner, out IDisposable disposable);
        public bool IsClaimed(string group, object owner);
    }
}
