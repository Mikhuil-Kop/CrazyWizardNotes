using System;
using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Creatures.Abstractions
{
    public interface ICreature
    {

    }
    
    public interface ICanWalk
    {
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
        public Emotion Emotion { get; set; }
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
        Disgusted = 7
    }

    public interface ICanEat
    {
        public float CaloriesToSaturation { get; }
        public float CaloriesAccumulated { get; }
        public float EatingMultiplier { get; }
        public void Eat(IEatable eatable, float deltaTime);
    }

    public interface IEatable
    {
        public float CaloriesMax { get; }
        public float CaloriesLeft { get; }
        public float EatingSpeed { get; }

        public float EatThis(ICanEat digester, float deltaTime);
    }

    public interface IClaimable
    {
        public bool TryClaim(string group, out IDisposable disposable);
        public bool IsClaimed(string group);
    }
}