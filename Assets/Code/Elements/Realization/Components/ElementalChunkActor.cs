using UNet.Common;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Elements.Realization
{
    public class ElementalChunkActor : IActor
    {
        public ISpaceChunk ParentSpaceChunk { get; }

        public ElementalChunkActor(ISpaceChunk spaceChunk)
        {
            ParentSpaceChunk = spaceChunk;
        }

        public IReadonlyTags Tags => new Tags();
        public T As<T>() where T : class => this as T;
        public bool Is<T>() where T : class => this is T;

        public float Temperature { get; set; } = 0f;
        public float DeltaTemperature { get; set; } = 0f;
    }
}
