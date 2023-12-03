using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UNet.Common
{
    public class IndexCollection<T> where T : class
    {
        //#region Основа

        //private LinkedList<T> AllObjects { get; set; }

        //private Stack<object> CollectionItems { get; set; }


        //public IndexCollection()
        //{
        //    AllObjects = new LinkedList<T>();
        //    CollectionItems = new Stack<SortedSet<T>>();
        //}

        //public void Add(T value)
        //{
        //    AllObjects.AddFirst(value);
        //}

        //public void Add(IEnumerable<T> values)
        //{
        //    foreach (var v in values)
        //    {
        //        Add(v);
        //    }
        //}

        //public void AddIndex<V>(string indexCode, Func<T, V> index)
        //{
        //    Collect
        //}

        //public void Recompile()
        //{

        //}

        //#endregion

        //#region Классы

        //private class IndexCollectionItem<V>
        //{
        //    public string IndexCode { get; set; }
        //    public BinaryTree<T> Tree { get; set; }
        //    public Func<T, V> Func { get; set; }
        //}

        //#endregion
    }
}