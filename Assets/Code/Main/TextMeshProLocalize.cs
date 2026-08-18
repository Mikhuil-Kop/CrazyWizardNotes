using TMPro;
using UNet.Core.Abstractions;
using UNet.UI.Abstractions;

namespace CrazyWizardNotes.Main
{
    public class TextMeshProLocalize : IHandlesStart
    {
        private ILocalizationService LocalizationService { get; }
        private TextMeshPro TextMesh { get; }

        public TextMeshProLocalize(ILocalizationService localizationService, TextMeshPro textMesh)
        {
            LocalizationService = localizationService;
            TextMesh = textMesh;
        }

        public void OnStart()
        {
            TextMesh.text = LocalizationService.GetLocalizedString(TextMesh.text);
            TextMesh.fontStyle = FontStyles.UpperCase;
        }
    }
}
