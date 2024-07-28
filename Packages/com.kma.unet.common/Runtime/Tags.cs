using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UNet.Common
{
    /// <summary>
    /// <para>
    /// Класс Tags представляет коллекцию уникальных строковых тегов.
    /// <para>
    /// </para>
    /// Поддерживает добавление, удаление, проверку наличия, перечисление,
    /// а также операции копирования, объединения, вычитания и пересечения.
    /// </para>
    /// <para>
    /// Все операции возвращают новые экземпляры, не изменяя исходный.
    /// </para>
    /// </summary>
    public class Tags : IEnumerable<string>
    {
        private HashSet<string> TagsSet { get; set; }

        /// <summary>
        /// Создаёт пустую коллекцию тегов.
        /// </summary>
        public Tags()
        {
            TagsSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Создаёт коллекцию тегов из переданной последовательности строк.
        /// Дубликаты игнорируются, сравнение без учёта регистра.
        /// </summary>
        /// <param name="tags">Исходные теги.</param>
        public Tags(IEnumerable<string> tags)
        {
            TagsSet = new HashSet<string>(tags ?? Enumerable.Empty<string>(), StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Добавляет тег в коллекцию. Если тег уже существует - ничего не делает.
        /// </summary>
        /// <param name="tag">Добавляемый тег. Не может быть null.</param>
        /// <exception cref="ArgumentNullException">Если tag равен null.</exception>
        public void Add(string tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            TagsSet.Add(tag);
        }

        /// <summary>
        /// Удаляет тег из коллекции. Если тега нет - ничего не делает.
        /// </summary>
        /// <param name="tag">Удаляемый тег. Не может быть null.</param>
        /// <exception cref="ArgumentNullException">Если tag равен null.</exception>
        public void Remove(string tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            TagsSet.Remove(tag);
        }

        /// <summary>
        /// Проверяет наличие тега в коллекции (сравнение без учёта регистра).
        /// </summary>
        /// <param name="tag">Проверяемый тег. Не может быть null.</param>
        /// <returns>true, если тег присутствует; иначе false.</returns>
        /// <exception cref="ArgumentNullException">Если tag равен null.</exception>
        public bool Contains(string tag)
        {
            if (tag == null) throw new ArgumentNullException(nameof(tag));
            return TagsSet.Contains(tag);
        }

        /// <summary>
        /// Возвращает новую коллекцию - точную копию текущей.
        /// </summary>
        public Tags Copy() => new(this);

        /// <summary>
        /// Объединение: возвращает новую коллекцию, содержащую все теги из текущей и из other.
        /// </summary>
        /// <param name="other">Коллекция тегов для объединения.</param>
        public Tags Union(Tags other) => new(TagsSet.Union(other?.TagsSet ?? Enumerable.Empty<string>()));

        /// <summary>
        /// Вычитание: возвращает новую коллекцию, содержащую теги текущей, отсутствующие в other.
        /// </summary>
        /// <param name="other">Коллекция тегов для вычитания.</param>
        public Tags Except(Tags other) => new(TagsSet.Except(other?.TagsSet ?? Enumerable.Empty<string>()));

        /// <summary>
        /// Пересечение: возвращает новую коллекцию, содержащую только общие теги.
        /// </summary>
        /// <param name="other">Коллекция тегов для пересечения.</param>
        public Tags Intersect(Tags other) => new(this.TagsSet.Intersect(other?.TagsSet ?? Enumerable.Empty<string>()));

        /// <summary>
        /// Оператор + : объединение двух коллекций (аналог Union).
        /// </summary>
        public static Tags operator +(Tags left, Tags right) => left.Union(right);

        /// <summary>
        /// Оператор - : вычитание правой коллекции из левой (аналог Except).
        /// </summary>
        public static Tags operator -(Tags left, Tags right) => left.Except(right);

        /// <summary>
        /// Оператор * : пересечение двух коллекций (аналог Intersect).
        /// </summary>
        public static Tags operator *(Tags left, Tags right) => left.Intersect(right);

        /// <summary>
        /// Оператор + : объединение двух коллекций (аналог Union).
        /// </summary>
        public static Tags operator +(Tags left, string right) => (Tags)left.Union(new string[] { right });

        /// <summary>
        /// Оператор - : вычитание правой коллекции из левой (аналог Except).
        /// </summary>
        public static Tags operator -(Tags left, string right) => (Tags)left.Except(new string[] { right });

        /// <summary>
        /// Возвращает перечислитель для всех тегов.
        /// </summary>
        public IEnumerator<string> GetEnumerator() => TagsSet.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Возвращает количество тегов в коллекции.
        /// </summary>
        public int Count => TagsSet.Count;
    }
}
