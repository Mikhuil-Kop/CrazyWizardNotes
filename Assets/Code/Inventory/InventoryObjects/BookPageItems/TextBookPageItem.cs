using System.Xml.Linq;
using TMPro;
using UnityEngine;

namespace CrazyWizardNotes.Inventory.OpenedBookPageItems
{
    public class TextBookPageItem : OpenedBookPageItemBase
    {
        [SerializeField]
        private TextMeshProUGUI Text;

        public override void SetInventoryData(XElement data)
        {
            base.SetInventoryData(data);

            Text.text = data.Element("Text").Value;
        }

        public override XElement GetInventoryData()
        {
            var data = base.GetInventoryData();
            data.Add(new XElement("Text", Text.text));

            return data;
        }
    }
}