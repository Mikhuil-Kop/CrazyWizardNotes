using UNet.UI.Abstractions;

namespace CrazyWizardNotes.UI.Abstractions
{
    /// <summary>
    /// Информация об заклинании, указанном на панели.
    /// </summary>
    public interface IActionBarServiceItem
    {
        public string ActionName { get; }
        public char ActionHotkey { get; }
        public TextString ActionTitle { get; }
        public TextString ActionDescription { get; }
        public bool IsEnabled { get; set; }
    }
}
