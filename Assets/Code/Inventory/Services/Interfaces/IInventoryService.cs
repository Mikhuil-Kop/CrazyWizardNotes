using System.Xml.Linq;

namespace CrazyWizardNotes.Inventory
{
    public interface IInventoryService
    {
        void Open(string name);
        void Close(string name);

        IInventoryObject LoadInventoryObject(string objectName, object parent);
        IInventoryObject LoadInventoryObject(XElement objectXml, object parent);
    }
}