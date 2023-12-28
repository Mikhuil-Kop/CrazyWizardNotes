using System.Linq;
using System.Collections.Generic;
using CrazyWizardNotes.Spells.Logic.SpellOperands;
using CrazyWizardNotes.Spells.Logic.SpellOperators;
using System;

namespace CrazyWizardNotes.Spells.Logic
{
    public static class SpellsExtension
    {
        public static float Multiply(this float value, IMultiplier multiplier)
        {
            return multiplier.Multiply(value);
        }

        public static int Multiply(this int value, IMultiplier multiplier)
        {
            return (int)multiplier.Multiply(value);
        }

        public static IEnumerable<float> Multiply(this IEnumerable<float> value, IMultiplier multiplier)
        {
            return value.Select(x => multiplier.Multiply(x));
        }

        public static IEnumerable<int> Multiply(this IEnumerable<int> value, IMultiplier multiplier)
        {
            return value.Select(x => (int)multiplier.Multiply(x));
        }

        public static bool? ConditionValue(this ISpellOperand operand)
        {
            return (operand as IConditionOperand)?.Value;
        }

        public static bool ConditionValue(this ISpellOperand operand, bool valueIfNull)
        {
            return (operand as IConditionOperand)?.Value ?? valueIfNull;
        }

        public static IEnumerable<object> ObjectsCollection(this ISpellOperand operand)
        {
            return (operand as IObjectOperand)?.Objects;
        }

        public static void MustBeNull(this object obj)
        {
            if (obj != null)
            {
                throw new Exceptions.SpellSyntaxException();
            }
        }

        public static IEnumerable<T> FindByTag<T>(this IEnumerable<T> enumerable, string tag)
        {
            return enumerable.Where(x =>
            {
                var tags = (x as ITaggable)?.Tags;

                if (tags == null)
                {
                    return false;
                }

                return tags.Contains(tag);
            });
        }

        public static IEnumerable<T> FindByTag<T>(this IEnumerable<T> enumerable, Func<List<string>, bool> selector)
        {
            return enumerable.Where(x =>
            {
                var tags = (x as ITaggable)?.Tags;

                if (tags == null)
                {
                    return false;
                }

                return selector(tags);
            });
        }
    }
}
