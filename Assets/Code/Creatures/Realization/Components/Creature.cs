using CrazyWizardNotes.Creatures.Abstractions;
using System;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Creatures.Realization
{
    public class Creature : ICreature, ICanWalk, ICanCarry, ICanAttack, ICanEat, IHasName, IHasEmotion, IHandlesFixedUpdate
    {
        public string Name { get; set; }
        public float CaloriesToSaturation { get; set; }
        public float CaloriesAccumulated { get; set; }
        public float EatingMultiplier { get; set; }
        public bool EatingIsFull => CaloriesAccumulated >= CaloriesToSaturation;
        public Emotion Emotion { get; set; }
        public IHasSpacePoint HasSpacePoint { get; set; }
        public IPhysical Physical { get; set; }

        public Creature(IHasSpacePoint hasSpacePoint, IPhysical physical)
        {
            Name = "123";
            CaloriesToSaturation = 2000f;
            CaloriesAccumulated = 250f; // 500f;
            EatingMultiplier = 1f;
            Emotion = Emotion.Neutral;
            HasSpacePoint = hasSpacePoint;
            Physical = physical;
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            Physical.ApplyBreak(1f, fixedDeltaTime);
        }

        public void WalkTo(SpacePoint spacePoint)
        {
            var direction = spacePoint.Position - HasSpacePoint.SpacePoint.Position;

            Physical.ApplyWalk(direction, 3f, 1f);
        }

        public void Attack()
        {
            throw new NotImplementedException();
        }

        public void Carry(ICarrieble carry)
        {
            throw new NotImplementedException();
        }

        public void Eat(IEatable eatable, float deltaTime)
        {
            CaloriesAccumulated += eatable.EatThis(this, deltaTime);
        }
    }
}