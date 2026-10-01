using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;

namespace CrazyWizardNotes.Elements.Realization
{
    public class ElementalChunkActorFactory : ISpaceChunkActorFactory
    {
        public IActor CreateActorForChunk(ISpaceChunk spaceChunk)
        {
            return new ElementalChunkActor(spaceChunk);
        }
    }
}
