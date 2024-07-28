using UNet.Core.Abstractions;
using UNet.Sound.Abstractions;

namespace UNet.Sound.Realisation
{
    public class SoundService : ISoundService
    {
        private IActorsCollectionService SubjectsCollectionService { get; set; }

        public event OnSoundRecievedEventHandler OnSoundRecieved;

        public SoundService(IActorsCollectionService actorsCollectionService)
        {
            SubjectsCollectionService = actorsCollectionService;
        }

        public void MakeSound(ISoundSource source, string sound, float volume)
        {
            var recievers = SubjectsCollectionService.Get<ISoundReciever>();

            var args = new OnSoundRecievedEventArgs()
            {
                Sound = sound,
                Volume = volume,
                Source = source
            };

            OnSoundRecieved?.Invoke(args);

            foreach (var reciever in recievers)
            {
                reciever.RecieveSound(source, sound, volume);
            }
        }
    }
}