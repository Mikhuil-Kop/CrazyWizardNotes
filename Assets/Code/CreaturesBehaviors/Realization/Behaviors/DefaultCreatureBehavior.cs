using CrazyWizardNotes.CreaturesBehaviors.Abstractions;
using System.Collections.Generic;
using System.Linq;
using UNet.Core.Abstractions;
using Mathf = UnityEngine.Mathf;

namespace CrazyWizardNotes.CreaturesBehaviors.Realization
{
    public class DefaultCreatureBehavior : CreatureBehaviorBase
    {
        #region Поля

        private IUServiceProvider ServiceProvider { get; }
        private IActor Actor { get; }
        private IEnumerable<ICreatureChoiceProvider> CreatureChoiceProviders { get; }

        #endregion

        #region Состояние

        private float PhysiologyNeeds { get; set;  }
        private float SafetyNeeds { get; set; }
        private float SocialNeeds { get; set; }
        private float LearningNeeds { get; set; }
        private float CreationNeeds { get; set; }

        #endregion

        #region Функционал

        public DefaultCreatureBehavior(IUServiceProvider serviceProvider, IActor actor)
        {
            ServiceProvider = serviceProvider;
            Actor = actor;

            CreatureChoiceProviders = new ICreatureChoiceProvider[]
            {
                new GoingForFoodChoiceProvider(ServiceProvider, Actor)
            };
        }

        protected override IEnumerable<ICreatureChoiceProvider> GetChoiceProviders()
        {
            return CreatureChoiceProviders;
        }

        protected override float ValueToFloat(ChoiceValue choice)
        {
            if (choice.NeedsValues.Length == 0)
            {
                return 0f;
            }

            Recalc();

            return choice.NeedsValues.Sum(NeedsValueToFloat);
        }

        protected virtual void Recalc()
        {
            PhysiologyNeeds = 1f;
            SafetyNeeds = 1f;
            SocialNeeds = 1f;
            LearningNeeds = 1f;
            CreationNeeds = 1f;
        }

        protected virtual float NeedsValueToFloat(NeedsValue value)
        {
            var physiologyNeeds = PhysiologyNeeds;
            var safetyNeeds = SafetyNeeds;
            var socialNeeds = SocialNeeds;
            var learningNeeds = LearningNeeds;
            var creationNeeds = CreationNeeds;

            // Базовое значение потребностей

            var physiologyValue =
                  value.Air
                + value.Water
                + value.Food
                + value.Sleep
                + value.Temperature
                + value.Pleasure;

            var safetyValue =
                  value.Safety;

            var socialValue =
                  value.Communication
                + value.Love;

            var learningValue =
                  value.Learning;

            var creationValue =
                  value.Creativity
                + value.Aesthetics
                + value.Nature;

            // Расставление приоритетов по пирамиде Маслоу

            safetyValue *= Prioritize(physiologyNeeds);
            socialValue *= Prioritize(physiologyNeeds, safetyNeeds);
            learningValue *= Prioritize(physiologyNeeds, safetyNeeds, socialNeeds);
            creationValue *= Prioritize(physiologyNeeds, safetyNeeds, socialNeeds, learningNeeds);

            // Применение модификаторов
            
            // Объединение

            return physiologyValue + safetyValue + socialValue + learningValue + creationValue;
        }

        // Чем выше f, тем меньше приоритет у следующего уровня.
        private float Prioritize(params float[] values)
        {
            var max = values.Max();

            return Mathf.Exp(-Mathf.Pow(Mathf.Max(max, 0f), 3f));
        }

        #endregion
    }
}
