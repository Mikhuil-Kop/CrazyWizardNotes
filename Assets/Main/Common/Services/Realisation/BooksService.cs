using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using CrazyWizardNotes.Books;
using UnityEngine;
using UnityEngine.UIElements;

namespace CrazyWizardNotes.Common.Services
{
    public class BooksService : SingletonBehavior<BooksService>
    {
        [SerializeField]
        private RectTransform BooksParent;
        [SerializeField]
        private OpenedBook OpenedBookTemplate;

        private Dictionary<string, IOpenedBook> OpenedBooks { get; set; }


        protected override void Awake()
        {
            base.Awake();

            OpenedBooks = new Dictionary<string, IOpenedBook>();
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
            var bookData = Resources.Load<BookData>("Books/" + bookName);
            var bookGameObject = Instantiate(OpenedBookTemplate.gameObject, Vector3.zero, Quaternion.identity, BooksParent);
            var book = gameObject.GetComponent<IOpenedBook>();

            FillBook(book, bookData);

            return book;
        }

        private void FillBook(IOpenedBook book, BookData bookData)
        {
            foreach (var page in bookData.Pages)
            {
                foreach (var item in page.Items)
                {

                }
            }
        }

        #region Сериализация

        private XElement SerializeBookData(BookData book)
        {
            var bookXml =
                new XElement("book",
                    new XAttribute("title", book.BookTitle),
                    new XElement("pages", book.Pages.Select(
                        (page, i) =>
                            new XElement("page",
                                new XAttribute("index", i),
                                new XElement("items", page.Items.Select(
                                    (item, j) =>
                                        new XElement("item",
                                            new XAttribute("index", j),
                                            new XAttribute("type", item.Type),
                                            new XAttribute("position", item.Position.x + "," + item.Position.y),
                                            new XAttribute("scale", item.Scale.x + "," + item.Scale.y),
                                            new XAttribute("params", string.Join(",", item.Parameters)))))))));

            return bookXml;
        }

        private BookData UnserializeBookData(string str)
        {
            var bookXml = XElement.Parse(str);

            var book = ScriptableObject.CreateInstance<BookData>();

            book.BookTitle = bookXml.Attribute("title").Value;
            book.Pages = bookXml.Elements("page").Select(pageXml =>
                new PagesData()
                {
                    Items = pageXml.Elements("item").Select(itemXml =>
                    new PageItemData()
                    {
                        Type = itemXml.Attribute("type").Value,
                        Position = ParseVector2((itemXml.Attribute("position").Value)),
                        Scale = ParseVector2((itemXml.Attribute("scale").Value)),
                        Parameters = itemXml.Attribute("params").Value.Split(',')
                    })
                    .ToArray()
                })
                .ToArray();

            return book;
        }

        private Vector2 ParseVector2(string str)
        {
            var split = str.Split(',');
            return new Vector2(int.Parse(split[0]), int.Parse(split[1]));
        }


        #endregion
    }
}