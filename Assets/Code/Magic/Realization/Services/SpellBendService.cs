using CrazyWizardNotes.Magic.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Magic.Realization
{
    public class SpellBendService : ISpellBendService, IHandlesUpdate
    {
        private ILogger Logger { get; }
        private IPhysicsService PhysicsService { get; }
        private Dictionary<(ISpellBending, object), SpellBendingInfo> Spells { get; }
        private float SpellCatchRadius { get; }

        public SpellBendService(ILogger logger, IPhysicsService physicsService)
        {
            Logger = logger;
            PhysicsService = physicsService;

            Spells = new Dictionary<(ISpellBending, object), SpellBendingInfo>();

            SpellCatchRadius = 2f;
        }

        public void StartBend(ISpellBending owner, object key, SpacePoint point)
        {
            Logger.LogInfo("StartBend - Begin.");

            if (owner == null)
            {
                throw new ArgumentNullException(nameof(owner));
            }

            EndBend(owner, key);

            var spells = PhysicsService.GetActors(new PhysicsConditionParams()
            {
                ShapeType = PhysicsShapeType.Circle,
                ShapeSize = new (SpellCatchRadius, SpellCatchRadius),
                ShapeCenter = point,
            });

            var mainSpell = spells.ActorsAs<ISpellBendable>().FirstOrDefault();

            if (mainSpell == null)
            {
                return;
            }

            var spellInfo = new SpellBendingInfo()
            {
                SpellBending = owner,
                SpellBendable = mainSpell,
                StartBendingPosition = point,
                TargetBendingPosition = point
            };

            Spells.Add(new(owner, key), spellInfo);

            InvokeOnStartBend(spellInfo);

            Logger.LogInfo("StartBend - End.");
        }

        public void UpdateBend(ISpellBending owner, object key, SpacePoint point)
        {
            Logger.LogInfo("UpdateBend - Begin.");

            if (owner == null)
            {
                throw new ArgumentNullException(nameof(owner));
            }

            var exists = Spells.TryGetValue(new(owner, key), out var spell);

            if (!exists)
            {
                return;
            }

            spell.TargetBendingPosition = point;

            Logger.LogInfo("UpdateBend - End.");
        }

        public void EndBend(ISpellBending owner, object key)
        {
            Logger.LogInfo("EndBend - Begin.");

            if (owner == null)
            {
                throw new ArgumentNullException(nameof(owner));
            }

            var removed = Spells.Remove(new(owner, key), out var spell);

            if (!removed)
            {
                return;
            }

            InvokeOnEndBend(spell);

            Logger.LogInfo("EndBend - End.");
        }

        public void EndBend(ISpellBending owner)
        {
            Logger.LogInfo("EndBend - Begin.");

            if (owner == null)
            {
                throw new ArgumentNullException(nameof(owner));
            }

            var keysToRemove = Spells.Keys.Where(x => x.Item1 == owner).ToArray();

            foreach (var key in keysToRemove)
            {
                EndBend(owner, key);
            }

            Logger.LogInfo("EndBend - End.");
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var spell in Spells)
            {
                InvokeOnUpdateBend(spell.Value);
            }
        }

        private void InvokeOnStartBend(SpellBendingInfo spell)
        {
            spell.SpellBendable.OnStartBend(new SpellBendStartEventArgs()
            {
                StartPoint = spell.StartBendingPosition
            });
        }

        private void InvokeOnUpdateBend(SpellBendingInfo spell)
        {
            spell.SpellBendable.OnUpdateBend(new SpellBendUpdateEventArgs()
            {
                TargetPoint = spell.TargetBendingPosition
            });
        }

        private void InvokeOnEndBend(SpellBendingInfo spell)
        {
            spell.SpellBendable.OnEndBend(new SpellBendEndEventArgs()
            {
                
            });
        }

        private class SpellBendingInfo
        {
            public ISpellBendable SpellBendable { get; set; }
            public ISpellBending SpellBending { get; set; }
            public SpacePoint StartBendingPosition { get; set; }
            public SpacePoint TargetBendingPosition { get; set; }
        }
    }
}