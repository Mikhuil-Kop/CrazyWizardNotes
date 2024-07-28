using CrazyWizardNotes.Magic.Abstractions;
using UNet.Core.Abstractions;
using UNet.Inputs.Abstractions;
using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Avatar.Realization
{
    public class WizardAvatar : IHandlesUpdate
    {
        private ISpellBending SpellBending { get; }
        private IPointerInputService InputService { get; }
        private IPhysicsService PhysicsService { get; }
        private ISpellBendService SpellBendService { get; }

        public void OnUpdate(float deltaTime)
        {

            if (InputService.GetButtonDown("SpellBend"))
            {
                SpellBendService.StartBend(SpellBending, null, PhysicsService.GetDefaultSpacePoint(InputService.Pointer));
            }

            if (InputService.GetButton("SpellBend"))
            {
                SpellBendService.UpdateBend(SpellBending, null, PhysicsService.GetDefaultSpacePoint(InputService.Pointer));
            }

            if (InputService.GetButtonUp("SpellBend"))
            {
                SpellBendService.EndBend(SpellBending, null);
            }
        }
    }
}
