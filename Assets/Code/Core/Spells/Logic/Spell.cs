using System;
using System.Collections.Generic;
using System.Linq;

namespace CrazyWizardNotes.Spells.Logic
{
    public class Spell : ISpell
    {
        #region События

        public event SpellOperatorDelegate OnStartCust;
        public event SpellOperatorDelegate OnTickCust;
        public event SpellOperatorDelegate OnEndCust;

        #endregion

        #region Поля

        private List<BinaryTree<ISpellOperator>> SpellOperators { get; set; }

        private SpellContext Context { get; set; }

        #endregion

        #region Функции

        public Spell(string spellText)
        {
            Context = new SpellContext()
            {
                Spell = this,
            };

            SpellOperators = ParseSpellString(spellText)
                .Select(x => ParseTree(x.ToArray(), x => x.Priority))
                .ToList();

            foreach (var operatorsRow in SpellOperators)
            {
                CompileTree(operatorsRow, Context);
            }
        }

        public void StartCast(float deltaTime)
        {
            Context.TimeFromStart = 0;
            Context.DeltaTime = deltaTime;
            Context.Ticks = 0;

            OnStartCust?.Invoke();
        }

        public void TickCust(float deltaTime)
        {
            Context.TimeFromStart += deltaTime;
            Context.DeltaTime = deltaTime;
            Context.Ticks++;

            OnTickCust?.Invoke();
        }

        public void EndCust(float deltaTime)
        {
            Context.DeltaTime = deltaTime;

            OnEndCust?.Invoke();
        }

        #endregion

        #region Вспомогательное

        private List<List<ISpellOperator>> ParseSpellString(string spellString)
        {
            return spellString
                .Split('\n')
                .Select(x => x
                    .Split(' ', ',', ';')
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Select(x =>
                    {
                        var typeParsed = x.Split(':', StringSplitOptions.RemoveEmptyEntries);
                        var name = typeParsed[0];
                        var param = typeParsed.Skip(1).ToArray();
                        var typeName = "CrazyWizardNotes.Spells.Logic.SpellOperators." + name + "Operator";
                        var type = Type.GetType(typeName);

                        return Activator.CreateInstance(type, param);
                    })
                    .Cast<ISpellOperator>()
                    .ToList())
                .ToList();
        }

        private BinaryTree<T> ParseTree<T>(T[] collection, Func<T, int> prioritySelector)
        {
            if (collection.Length == 0)
            {
                return null;
            }

            var maxPriority = collection.Min(x => prioritySelector(x));
            var index = collection.IndexOf(x => prioritySelector(x) == maxPriority);

            var value = collection[index];
            var left = collection.BeforeIndex(index).ToArray();
            var right = collection.AfterIndex(index).ToArray();

            return new BinaryTree<T>(value, ParseTree(left, prioritySelector), ParseTree(right, prioritySelector));
        }

        private SpellOperatorDelegate CompileTree(BinaryTree<ISpellOperator> nodes, SpellContext context)
        {
            if(nodes == null)
            {
                return null;
            }

            return nodes.Value.Compile(CompileTree(nodes.Left, context), CompileTree(nodes.Right, context), context);
        }

        #endregion
    }
}

