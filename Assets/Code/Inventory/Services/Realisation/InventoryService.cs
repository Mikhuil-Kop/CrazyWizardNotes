using System;
using System.Collections.Generic;
using System.Xml.Linq;
using CrazyWizardNotes.Common;
using CrazyWizardNotes.Common.Services;
using UnityEngine;

namespace CrazyWizardNotes.Inventory
{
    public class InventoryService : SingletonBehavior<IInventoryService>, IInventoryService
    {
        [SerializeField]
        private RectTransform InventoryParent;

        private Dictionary<string, IInventoryObject> InventoryItems { get; set; }

        protected override void Awake()
        {
            base.Awake();

            InventoryItems = new Dictionary<string, IInventoryObject>();
        }

        // TEST
        protected void Start()
        {
            Open("TestBook");
            Open("TestBook1");
            Open("TestBook2");
            Open("TestPotion");
        }

        protected void Update()
        {
            var inputService = InputService.Instance;

            if (inputService.GetButtonDown("mouse"))
            {
            }

            if (inputService.GetButton("mouse"))
            {
            }

            if (inputService.GetButtonUp("mouse"))
            {
            }
        }

        public void Open(string bookName)
        {
            IInventoryObject book = null;

            if (!InventoryItems.TryGetValue(bookName, out book))
            {
                book = LoadInventoryObject(bookName, InventoryParent);
                InventoryItems.Add(bookName, book);
            }

            book.ToTop();
        }

        public void Close(string bookName)
        {
            throw new NotImplementedException();
        }

        public IInventoryObject LoadInventoryObject(string objectName, object parent)
        {
            var data = Resources.Load<XmlData>("InventoryData/" + objectName.Replace('.', '/'));
            var xml = XElement.Parse(data.XML);

            return LoadInventoryObject(xml, parent);
        }

        public IInventoryObject LoadInventoryObject(XElement objectXml, object parent)
        {
            string resourceName = "";
            GameObject resource = null;
            GameObject gameObject = null;
            IInventoryObject inventoryObject = null;

            try
            {
                resourceName = "InventoryObjects/" + objectXml.Attribute("Type").Value.Replace('.', '/');
                resource = Resources.Load<GameObject>(resourceName);
                gameObject = Instantiate(resource, Vector3.zero, Quaternion.identity, parent as Transform);
                inventoryObject = gameObject.GetComponent<IInventoryObject>();

                inventoryObject.SetInventoryData(objectXml);

                return inventoryObject;
            }
            catch
            {
                Debug.LogError("Ошибка при загрузке ресурса\n" +
                    $"\tobjectXml={objectXml.Name}\n" +
                    $"\tresourceName={resourceName}\n" +
                    $"\tresource={resource}\n" +
                    $"\tgameObject={gameObject}\n" +
                    $"\tinventoryObject={inventoryObject}", this);

                throw;
            }
        }
    }
}