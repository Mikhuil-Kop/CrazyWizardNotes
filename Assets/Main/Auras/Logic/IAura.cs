using System.Threading.Tasks;

namespace CrazyWizardNotes.Auras.Logic
{
    public interface IAura
    {
        int Height { get; }
        int Width { get; }

        Task UpdateAsync();
        AuraColor[] GetAuraColors();
    }
}