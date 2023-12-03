using System.Linq;
using System.Collections.Generic;
using CrazyWizardNotes.Spells.Logic.SpellOperands;
using CrazyWizardNotes.Spells.Logic.SpellOperators;

namespace CrazyWizardNotes.Spells.Logic
{
    public static class SpellsExtension
    {
        //public static IEnumerable<T> OfType<T, V>(this IEnumerable<V> values) where T : class
        //{
        //    return values.Select(x => x as T).Where(x => x != null);
        //}

        public static IEnumerable<T> Where<T>(this IEnumerable<T> values, IIndex index)
        {
            if (index == null)
            {
                return values;
            }

            return index.Filter(values);
        }

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

        public static void Invoke(this IEffect effect)
        {
            effect.Action.Invoke(effect.Target);
        }

        public static bool? ConditionValue(this ISpellOperand operand)
        {
            return (operand as IConditionOperand)?.Value;
        }

        public static bool ConditionValue(this ISpellOperand operand, bool valueIfNull)
        {
            return (operand as IConditionOperand)?.Value ?? valueIfNull;
        }
    }
}
