using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using CrazyWizardNotes.Inventory;
using UnityEngine;
using UnityEngine.UIElements;

namespace CrazyWizardNotes.Common.Services
{
    public class BooksService : SingletonBehavior<IBooksService>, IBooksService
    {
        [SerializeField]
        private RectTransform BooksParent;

        private Dictionary<string, IOpenedBook> OpenedBooks { get; set; }

        protected override void Awake()
        {
            base.Awake();

            OpenedBooks = new Dictionary<string, IOpenedBook>();
        }

        // TEST
        protected void Start()
        {
            OpenBook("TestBook");
        }

        public void OpenBook(string bookName)
        {
            IOpenedBook book = null;

            if (!OpenedBooks.TryGetValue(bookName, out book))
            {
                book = LoadBook(bookName);
                OpenedBooks.Add(bookName, book);
            }

            book.ToTop();
        }

        public void CloseBook(string bookName)
        {


        }

        private IOpenedBook LoadBook(string bookName)
        {
            var bookData = Resources.Load<BookData>("BooksData/" + bookName);
            var bookResource = Resources.Load<GameObject>("OpenedBooks/" + bookData.Type);
            var bookGameObject = Instantiate(bookResource, Vector3.zero, Quaternion.identity, BooksParent);
            var book = bookGameObject.GetComponent<IOpenedBook>();

            book.SetBookData(bookData);

            return book;
        }

        #region Сериализация

        //private XElement SerializeBookData(BookData book)
        //{
        //    var bookXml =
        //        new XElement("book",
        //            new XAttribute("title", book.Type),
        //            new XElement("pages", book.Pages.Select(
        //                (page, i) =>
        //                    new XElement("page",
        //                        new XAttribute("index", i),
        //                        new XElement("items", page.Items.Select(
        //                            (item, j) =>
        //                                new XElement("item",
        //                                    new XAttribute("index", j),
        //                                    new XAttribute("type", item.Type),
        //                                    new XAttribute("position", item.Position.x + "," + item.Position.y),
        //                                    new XAttribute("scale", item.Scale.x + "," + item.Scale.y),
        //                                    new XAttribute("params", string.Join(",", item.Parameters)))))))));

        //    return bookXml;
        //}

        //private BookData UnserializeBookData(string str)
        //{
        //    var bookXml = XElement.Parse(str);

        //    var book = ScriptableObject.CreateInstance<BookData>();

        //    book.BookTitle = bookXml.Attribute("title").Value;
        //    book.Pages = bookXml.Elements("page").Select(pageXml =>
        //        new PageData()
        //        {
        //            Items = pageXml.Elements("item").Select(itemXml =>
        //            new PageItemData()
        //            {
        //                Type = itemXml.Attribute("type").Value,
        //                Position = ParseVector2((itemXml.Attribute("position").Value)),
        //                Scale = ParseVector2((itemXml.Attribute("scale").Value)),
        //                Parameters = itemXml.Attribute("params").Value.Split(',')
        //            })
        //            .ToArray()
        //        })
        //        .ToArray();

        //    return book;
        //}

        private Vector2 ParseVector2(string str)
        {
            var split = str.Split(',');
            return new Vector2(int.Parse(split[0]), int.Parse(split[1]));
        }

        #endregion
    }
}