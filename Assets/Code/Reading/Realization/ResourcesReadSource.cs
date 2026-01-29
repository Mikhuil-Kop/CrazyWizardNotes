using CrazyWizardNotes.Reading.Abstractions;
using System.Linq;
using UNet.Core.Abstractions;

namespace CrazyWizardNotes.Reading.Realization
{
    public class ResourcesReadSource : IReadSource
    {
        public IResourcesService ResourcesService { get;}
        public string ResourceName { get; }
        public ReadText ResourceText { get; set; }

        public ResourcesReadSource(IResourcesService resourcesService, [FromKey("ReadResourceName")]string resourceName)
        {
            ResourcesService = resourcesService;
            ResourceName = resourceName;
        }

        public ReadText GetReadText(IReadHandler reading)
        {
            if (ResourceText == null)
            {
                var resource = ResourcesService.Load<ReadTextResource>(ResourceName);

                ResourceText = Convert(resource);
            }

            // #{123}
            return ResourceText;
        }

        private static ReadText Convert(ReadTextResource text)
        {
            return new ReadText
            {
                Items = text.Items.Select(Convert).ToArray()
            };
        }

        private static ReadTextItem Convert(ReadTextItemResource item)
        {
            return new ReadTextItem
            {
                Text = item.Text,
                Language = item.Language
            };
        }
    }
}
