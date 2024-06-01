using System.Xml.Linq;
using TMPro;
using UnityEngine;

namespace CrazyWizardNotes.Inventory.OpenedBooks
{
    public class DefaultOpenedBook : OpenedBookBase
    {
        [SerializeField]
        private TextMeshProUGUI TitleText;

        public override void SetInventoryData(XElement data)
        {
            base.SetInventoryData(data);

            TitleText.text = data.Element("Title").Value;
        }

        public override XElement GetInventoryData()
        {
            var data =  base.GetInventoryData();
            data.Add(new XElement("Title", TitleText.text));

            return data;
        }
    }
}