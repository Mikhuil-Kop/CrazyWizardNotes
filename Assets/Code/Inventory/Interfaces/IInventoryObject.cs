using System.Xml.Linq;
using CrazyWizardNotes.Interface;

namespace CrazyWizardNotes.Inventory
{
    public interface IInventoryObject : IDragger
    {
        XElement GetInventoryData();
        void SetInventoryData(XElement data);
    }
}