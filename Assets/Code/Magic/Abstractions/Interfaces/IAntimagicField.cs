namespace CrazyWizardNotes.Magic.Abstractions
{
    public interface IAntimagicField
    {
        bool IsActive { get; set; }
        float Radius { get; set; }
    }
}
