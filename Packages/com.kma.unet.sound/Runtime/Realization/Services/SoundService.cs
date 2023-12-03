using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;
using UNet.Sound.Abstractions;

namespace UNet.Sound.Realization
{
    public class SoundService : ISoundService
    {
        private IActorsCollectionService ActorsCollectionService { get; set; }
        private IPhysicsService PhysicsService { get; set; }

        public event OnSoundRecievedEventHandler OnSoundRecieved;

        public SoundService(IActorsCollectionService actorsCollectionService, IPhysicsService physicsService)
        {
            ActorsCollectionService = actorsCollectionService;
            PhysicsService = physicsService;
        }

        public void MakeSound(ISoundSource source, string sound, float volume)
        {
            UnityEngine.Debug.Log($"SoundService: Making sound '{sound}' from source '{source}' with volume '{volume}'");

            var recievers = ActorsCollectionService.Get<ISoundReceiver>();

            var args = new OnSoundRecievedEventArgs()
            {
                Sound = sound,
                Volume = volume,
                Source = source,
            };

            OnSoundRecieved?.Invoke(args);

            foreach (var reciever in recievers)
            {
                args.Distance = PhysicsService.GetDistance(source.SoundSourcePosition, reciever.SoundRecieverPosition).Magnitude;

                reciever.RecieveSound(source, sound, volume);
            }
        }
    }
}