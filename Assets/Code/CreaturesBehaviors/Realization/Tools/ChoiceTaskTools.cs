using CrazyWizardNotes.Creatures.Abstractions;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.CreaturesBehaviors.Realization
{
    /// <summary>
    /// Класс с инструментами, упрощающими работу с CreatureChoice-ами.
    /// Поведение объектов - самая сложная часть системы с огромным количеством входных параметров.
    /// Любые CreatureChoice-и должны быть Fail Safe.
    /// </summary>
    internal static class CreatureChoiceTools
    {
        internal static bool TryWalk(ICanWalk walk, IHasSpacePoint point)
        {
            if (walk == null || point == null)
            {
                return false;
            }

            walk.WalkTo(point.SpacePoint);

            return true;
        }

        internal static bool TryEat(ICanEat eats, IActor food)
        {
            if (eats == null || food == null)
            {
                return false;
            }

            eats.StartEat(food);

            return true;
        }
    }
}
