using CrazyWizardNotes.Creatures.Abstractions;
using CrazyWizardNotes.CreaturesBehaviors.Abstractions;
using System.Collections.Generic;
using System.Linq;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.CreaturesBehaviors.Realization
{
    public sealed class GoingForFoodChoiceProvider : ICreatureChoiceProvider
    {
        private IUServiceProvider ServiceProvider { get; }
        private IActorsCollectionService ActorsService { get; }
        private IActor Creature { get; }

        public GoingForFoodChoiceProvider(IUServiceProvider serviceProvider, IActor creature)
        {
            ServiceProvider = serviceProvider;
            Creature = creature;

            ActorsService = serviceProvider.GetService<IActorsCollectionService>();
        }

        public IEnumerable<ICreatureChoiceInfo> GetChoices()
        {
            return ActorsService.GetAll()
                .WithComponents<IEatable, IHasSpacePoint>()
                .Where(x => x.AllComponentsPresent && x.Actor != Creature)
                .Select(x => GetChoice(x.Actor));
        }

        private ICreatureChoiceInfo GetChoice(IActor target)
        {
            return new GoingForFoodChoiceInfo(new()
            {
                Services = new()
                {
                    PhysicsService = ServiceProvider.GetRequiredService<IPhysicsService>()
                },

                Creature = new()
                {
                    Actor = Creature,
                    CanEat = Creature.As<ICanEat>(),
                    CanWalk = Creature.As<ICanWalk>(),
                    HasSpacePoint = Creature.As<IHasSpacePoint>(),
                },

                Target = new()
                {
                    Actor = target,
                    Eatable = target.As<IEatable>(),
                    Creature = target.As<ICreature>(),
                    Claimable = target.As<IClaimable>(),
                    Destroyable = target.As<IDestroyable>(),
                    HasSpacePoint = target.As<IHasSpacePoint>(),
                }
            });
        }
    }
}