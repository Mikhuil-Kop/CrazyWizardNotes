using System.Xml.Linq;

namespace CrazyWizardNotes.Inventory
{
    public interface IInventorizable
    {
        XElement GetInventoryData();
        void SetInventoryData(XElement data);
    }
}