using UNet.Core.Abstractions;

namespace CrazyWizardNotes.Scenes.Abstractions
{
    public interface ISceneRule
    {
        bool IsCompleted { get; }
        bool IsFailed { get; }

        void OnStart(IUServiceProvider serviceProvider);
        void OnTick();
    }
}
