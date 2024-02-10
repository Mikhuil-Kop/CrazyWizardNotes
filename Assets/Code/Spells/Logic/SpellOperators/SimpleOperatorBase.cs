using System;
using System.Linq;
using System.Reflection;
using CrazyWizardNotes.Spells.Logic.Exceptions;

namespace CrazyWizardNotes.Spells.Logic.SpellOperators
{
    public abstract class SimpleOperatorBase : ISpellOperator
    {
        public abstract int Priority { get; }

        public SpellOperatorDelegate Compile(SpellOperatorDelegate operator1, SpellOperatorDelegate operator2, SpellContext context)
        {
            var functions = GetOperatorFunctions();

            return () =>
            {
                var val1 = operator1?.Invoke();
                var val2 = operator2?.Invoke();

                var function = functions
                    .FirstOrDefault(x =>
                        x.MethodParam1.ParameterType.IsAssignableFrom(val1.GetType())
                        && x.MethodParam2.ParameterType.IsAssignableFrom(val2.GetType()))
                    ?? throw new SpellSyntaxException();

                return function.MethodInfo.Invoke(this, new object[] { val1, val2, context }) as ISpellOperand;
            };
        }

        private OperatorFunctionDto[] GetOperatorFunctions()
        {
            var type = GetType();
            var methods = type.GetMethods();
            var operations = methods
                .Select(x =>
                    new OperatorFunctionDto()
                    {
                        MethodInfo = x,
                        MethodParam1 = x.GetParameters()[0],
                        MethodParam2 = x.GetParameters()[1],
                        Attribute = x.GetCustomAttribute<OperatorFunctionAttribute>()
                    }
                )
                .Where(x => x.Attribute != null)
                .OrderByDescending(x => x.Attribute.Prioroty)
                .ToArray();

            return operations;
        }

        private class OperatorFunctionDto
        {
            public MethodInfo MethodInfo { get; set; }
            public ParameterInfo MethodParam1 { get; set; }
            public ParameterInfo MethodParam2 { get; set; }
            public OperatorFunctionAttribute Attribute { get; set; }
        }

        [AttributeUsage(AttributeTargets.Method)]
        protected class OperatorFunctionAttribute : Attribute
        {
            public int Prioroty { get; set; }

            public OperatorFunctionAttribute(int prioroty)
            {
                Prioroty = prioroty;
            }
        }
    }
}
