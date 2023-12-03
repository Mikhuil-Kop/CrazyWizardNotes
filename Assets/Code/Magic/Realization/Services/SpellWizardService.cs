using CrazyWizardNotes.Magic.Abstractions;
using System;
using UNet.Core.Abstractions;
using UNet.Inputs.Abstractions;
using UNet.Physics.Abstractions;
using InputAction = UnityEngine.InputSystem.InputAction;
using InputSystem = UnityEngine.InputSystem.InputSystem;
using Vector2 = UnityEngine.Vector2;

namespace CrazyWizardNotes.Magic.Realization
{
    [Obsolete("Криво сделал")]
    public class SpellWizardService : ISpellWizardService, ISpellBending, IHandlesUpdate
    {
        private ILogger Logger { get; }
        private IPointerInputService InputService { get; }
        private ISpellBendService SpellBendService { get; }
        private IPhysicsService PhysicsService { get; }
        private ISpellParticleService SpellParticleService { get; }

        private InputAction MoveAction { get; }
        private InputAction BendAction { get; }
        private InputAction InspectAction { get; }
        private InputAction[] SpellActions { get; }

        public SpellWizardService(ILogger logger, IPointerInputService inputService, ISpellBendService spellBendService, IPhysicsService physicsService, ISpellParticleService spellParticleService)
        {
            Logger = logger;
            InputService = inputService;
            SpellBendService = spellBendService;
            PhysicsService = physicsService;
            SpellParticleService = spellParticleService;

            MoveAction = InputSystem.actions.FindAction("Move");
            BendAction = InputSystem.actions.FindAction("Bend");
            InspectAction = InputSystem.actions.FindAction("Inspect");
            SpellActions = new InputAction[10];

            for (int i = 0; i < 10; i++)
            {
                SpellActions[i] = InputSystem.actions.FindAction("Spell_" + (i + 1));
            }
        }

        public void OnUpdate(float deltaTime)
        {
            if (MoveAction.WasPressedThisFrame())
            {
                UnityEngine.Debug.Log("--Move + " + MoveAction.ReadValue<Vector2>());
            }

            if (InspectAction.WasPressedThisFrame())
            {
                UnityEngine.Debug.Log("--Inspect");
            }

            if (BendAction.WasPressedThisFrame())
            {
                SpellBendService.StartBend(this, null, PhysicsService.GetDefaultSpacePoint(InputService.Pointer));
            }

            if (BendAction.IsPressed())
            {
                SpellBendService.UpdateBend(this, null, PhysicsService.GetDefaultSpacePoint(InputService.Pointer));
            }

            if (BendAction.WasReleasedThisFrame())
            {
                SpellBendService.EndBend(this, null);
            }

            for (int i = 0; i < 10; i++)
            {
                var spellAction = SpellActions[i];

                if (spellAction.WasPressedThisFrame())
                {
                    UnityEngine.Debug.Log("--" + spellAction.name);
                    OnSpellAction(i);
                }
            }
        }

        private void OnSpellAction(int spellId)
        {

        }
    }
}
