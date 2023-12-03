using CrazyWizardNotes.CreaturesBehaviors.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CrazyWizardNotes.CreaturesBehaviors.Realization
{
    /// <summary>
    /// Базовый класс поведения существа, работающий по принципу Метода Анализа Иерархий.
    /// </summary>
    public abstract class CreatureBehaviorBase : ICreatureBehavior, IDisposable
    {
        private List<ICreatureChoiceProvider> ChoicesProviders { get; set; }

        public ICreatureChoiceTask GetNewTask()
        {
            ChoicesProviders ??= GetChoiceProviders().ToList();

            var maxValue = float.MinValue;
            ICreatureChoiceInfo maxChoice = null;

            foreach (var choice in ChoicesProviders.SelectMany(x => x.GetChoices()))
            {
                var floatValue = ValueToFloat(choice.GetChoiceValue());

                if (floatValue > maxValue)
                {
                    maxValue = floatValue;
                    maxChoice = choice;
                }
            }

            return maxChoice?.GetChoiceTask();
        }

        protected abstract IEnumerable<ICreatureChoiceProvider> GetChoiceProviders();
        protected abstract float ValueToFloat(ChoiceValue choice);

        public void Dispose()
        {
            ChoicesProviders?.DisposeAll();
            ChoicesProviders?.Clear();
        }
    }
}
