using CrazyWizardNotes.Magic.Abstractions;
using CrazyWizardNotes.Totems.Abstractions;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Totems.Realization
{
    public class AntimagicTotem : TotemBase
    {
        private IPhysicsActorSpawnService SpawnService { get; }
        private IHasSpacePoint HasSpacePoint { get; }
        private AntimagicTotemConfig Config { get; }

        private IActor AntimagicActor { get; set; }
        private IAntimagicField AntimagicField { get; set; }

        public AntimagicTotem(IActor actor, ITotemVisualizer totemVisualizer, IPhysicsActorSpawnService spawnService, IHasSpacePoint hasSpacePoint, AntimagicTotemConfig config) : base(actor, totemVisualizer)
        {
            SpawnService = spawnService;
            HasSpacePoint = hasSpacePoint;
            Config = config;

            OnActivated += AntimagicTotem_OnActivated;
            OnDeactivated += AntimagicTotem_OnDeactivated;

            IsEnabled = Config.IsEnabled;
            IsActive = Config.IsActive;
            ActivationTimeGap = System.TimeSpan.MaxValue;
        }

        private void AntimagicTotem_OnActivated(TotemActivationEventArgs args)
        {
            if (AntimagicActor == null)
            {
                var spawn = SpawnService.SpawnActor("Magic/Spawns/AntimagicField", HasSpacePoint.SpacePoint);

                AntimagicActor = spawn;
                AntimagicField = spawn.AsRequired<IAntimagicField>();
            }

            AntimagicField.Radius = Config.AntimagicRadius;
            AntimagicField.IsActive = true;
        }

        private void AntimagicTotem_OnDeactivated(TotemActivationEventArgs args)
        {
            AntimagicField.IsActive = false;
        }
    }
}
