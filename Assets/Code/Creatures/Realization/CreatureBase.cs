using CrazyWizardNotes.Creatures.Abstractions;
using System;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Creatures.Realization
{
    public class CreatureBase : ICreature, ICanWalk, ICanCarry, ICanAttack, IHasName, IHasEmotion, ICanEat, ICarrieble, IHandlesStart, IHandlesDestroy, IDisposable, IHandlesUpdate
    {
        private IAsyncJobService AsyncJobService { get; }
        private IRegistredJob Job { get; set;  }
        private IActor Actor { get; }
        private SpacePoint TargetWalkPoint { get; set; }

        public string Name { get; private set; }
        public float CaloriesToSaturation { get; set; }
        public float CaloriesAccumulated { get; set; }
        public float EatingMultiplier { get; }
        public Emotion Emotion { get; set; }


        public void OnStart()
        {
            Job = AsyncJobService.Register(OnCharacterMindEvent, 1f);
        }

        public void OnDestroy()
        {
            Job?.Cancel();
        }

        public void Dispose()
        {
            Job?.Dispose();
        }

        public void WalkTo(SpacePoint spacePoint)
        {
            TargetWalkPoint = spacePoint;
        }

        public void Attack()
        {
            throw new System.NotImplementedException();
        }

        public void Carry(ICarrieble carry)
        {
            throw new System.NotImplementedException();
        }

        public void Eat(IEatable eatable, float deltaTime)
        {
            CaloriesAccumulated += eatable.EatThis(this, deltaTime);
        }

        /* -------------------------------------------------------------------------------------------------------------------------------------------------------------- */

        public void OnCharacterMindEvent()
        {

        }

        public void OnUpdate(float deltaTime)
        {
        }
    }
}