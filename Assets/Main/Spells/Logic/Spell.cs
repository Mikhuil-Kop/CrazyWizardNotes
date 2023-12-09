using System;
using System.Linq;
using System.Collections.Generic;

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

        private List<BinaryTree<ISpellNode>> SpellNodes { get; set; }

        private SpellContext Context { get; set; }

        #endregion

        #region Функции

        public Spell(IEnumerable<IEnumerable<ISpellNode>> list)
        {
            Context = new SpellContext()
            {
                Spell = this,
            };

            SpellNodes = list.Select(x => Parse(x.ToArray())).ToList();

            foreach (var nodes in SpellNodes)
            {
                ExecuteTree(nodes, Context);
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

        private BinaryTree<ISpellNode> Parse(ISpellNode[] collection)
        {
            if (collection.Length == 0)
            {
                return null;
            }

            var maxPriority = collection.Min(x => x.Priority);
            var index = collection.IndexOf(x => x.Priority == maxPriority);

            var value = collection[index];
            var left = collection.BeforeIndex(index).ToArray();
            var right = collection.AfterIndex(index).ToArray();

            return new BinaryTree<ISpellNode>(value, Parse(left), Parse(right));
        }

        private SpellOperatorDelegate ExecuteTree(BinaryTree<ISpellNode> nodes, SpellContext context)
        {
            if(nodes == null)
            {
                return null;
            }

            var o = nodes.Value.CreateSpellOperator();

            return o.Execute(ExecuteTree(nodes.Left, context), ExecuteTree(nodes.Right, context), context);
        }

        #endregion
    }
}

