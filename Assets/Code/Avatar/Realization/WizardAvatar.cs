using CrazyWizardNotes.Magic.Abstractions;
using CrazyWizardNotes.Reading.Abstractions;
using System.Linq;
using UNet.Core.Abstractions;
using UNet.Inputs.Abstractions;
using UNet.Physics.Abstractions;
using UNet.Sound.Abstractions;
using UnityEngine;

namespace CrazyWizardNotes.Avatar.Realization
{
    /// <summary>
    /// Класс игрового персонажа, отвечающий за взаимодействие игрока с миром.
    /// </summary>
    public class WizardAvatar : IHandlesUpdate, ISpellBending
    {
        private IPointerInputService InputService { get; }
        private ISpellBendService SpellBendService { get; }
        private IPhysicsService PhysicsService { get; }
        private ISoundService SoundService { get; }

        private ISoundSource SoundSource { get; }
        private IReadHandler ReadHandler { get; }

        private bool InteractionType { get; set; }

        public WizardAvatar(IPointerInputService inputService, IPhysicsService physicsService, ISpellBendService spellBendService, ISoundService soundService, ISoundSource soundSource, IReadHandler readHandler)
        {
            InputService = inputService;
            PhysicsService = physicsService;
            SpellBendService = spellBendService;
            SoundService = soundService;
            SoundSource = soundSource;
            ReadHandler = readHandler;
        }

        public void OnUpdate(float deltaTime)
        {
            // Смена типа взаимодействия
            if (InputService.GetButtonDown("Interaction Next"))
            {
                Debug.Log("Switching interaction type");
                InteractionType = !InteractionType;
            }

            // Чтение
            if (!InteractionType)
            {
                if (InputService.GetButtonDown("Interact 1"))
                {
                    Debug.Log("Interacting with object");
                    var pointed = InputService.PointedTargets;
                    var readable = pointed.ActorsAs<IReadSource>().FirstOrDefault();
                    var text = readable?.GetReadText(ReadHandler);

                    if (text != null)
                    {
                        SoundService.MakeSound(SoundSource, ReadHandler.GetKnownText(text), 1f);
                    }
                }
            }
            // Манипуляция магией
            else
            {
                if (InputService.GetButtonDown("Interact 1"))
                {
                    SpellBendService.StartBend(this, null, PhysicsService.GetDefaultSpacePoint(InputService.Pointer));
                }

                if (InputService.GetButton("Interact 1"))
                {
                    SpellBendService.UpdateBend(this, null, PhysicsService.GetDefaultSpacePoint(InputService.Pointer));
                }

                if (InputService.GetButtonUp("Interact 1"))
                {
                    SpellBendService.EndBend(this, null);
                }
            }
        }
    }
}
