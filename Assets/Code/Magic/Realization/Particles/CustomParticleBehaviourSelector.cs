using CrazyWizardNotes.Magic.Realization.Particles;
using System;
using System.Collections.Generic;
using System.Linq;
using UNet.Core.Abstractions;

namespace CrazyWizardNotes.Magic.Realization
{
    public class CustomParticleBehaviourSelector : IBehaviourSelector
    {
        private IKeyedServiceProvider ServiceProvider { get; }
        private static List<SelectorRule> Rules { get; }

        public CustomParticleBehaviourSelector(IKeyedServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public CustomParticleBehaviourBase GetBehaviour(HashSet<string> words)
        {
            foreach (var rule in Rules)
            {
                if (rule.Rule(words))
                {
                    return ServiceProvider.GetKeyedService<CustomParticleBehaviourBase>(rule.SpellName);
                }
            }

            throw new InvalidOperationException("No matching behaviour found for the given words: " + string.Join(", ", words));
        }

        #region Правила

        static CustomParticleBehaviourSelector()
        {

            Rules = new List<SelectorRule>()
            {
                new ("FireStorm",   x => All(x, "Fire", "Gravity")),
                new ("Fireball",    x => AllAndOnly(x, "Fire")),
                new ("GravityPush", x => AllAndOnly(x, "Gravity")),
                new ("Crash",       x => true),
            };

            // Should change rules type for modifications support
        }

        private static bool Is(HashSet<string> h, string s) => h.Contains(s);
        private static bool Not(HashSet<string> h, string s) => !h.Contains(s);
        private static bool All(HashSet<string> h, params string[] s) => s.All(x => h.Contains(x));
        private static bool Any(HashSet<string> h, params string[] s) => s.Any(x => h.Contains(x));
        private static bool Only(HashSet<string> h, params string[] s) => h.All(x => s.Contains(x));
        private static bool AllAndOnly(HashSet<string> h, params string[] s) => h.Count == s.Length && All(h, s);

        private class SelectorRule
        {
            public Func<HashSet<string>, bool> Rule { get; }
            public string SpellName { get; }

            public SelectorRule(string spellName, Func<HashSet<string>, bool> rule)
            {
                Rule = rule;
                SpellName = spellName;
            }
        }

        #endregion
    }
}

