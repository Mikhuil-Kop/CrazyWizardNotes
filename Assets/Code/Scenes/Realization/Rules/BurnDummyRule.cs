using CrazyWizardNotes.Elements.Abstractions;
using CrazyWizardNotes.Scenes.Abstractions;
using System.Linq;
using UNet.Core.Abstractions;

namespace CrazyWizardNotes.Scenes.Realization
{
    public class BurnDummyRule : ISceneRule
    {
        public bool IsCompleted { get; private set; }
        public bool IsFailed { get; private set; }
        private IActor Dummy { get; set; }

        public void OnStart(IUServiceProvider serviceProvider)
        {
            UnityEngine.Debug.Log("BurnDummyRule - OnStart");

            var actors = serviceProvider.GetRequiredService<IActorsCollectionService>();

            Dummy = actors.GetAll().FirstOrDefault(x => x.Is<IFlammable>());
        }

        public void OnTick()
        {
            UnityEngine.Debug.Log("BurnDummyRule - OnTick");

            var flammable = Dummy.As<IFlammable>();

            UnityEngine.Debug.Log($"BurnDummyRule IsBurning - {flammable.IsBurning}");
            if (flammable.IsBurning)
            {
                IsCompleted = true;
            }
        }
    }
}