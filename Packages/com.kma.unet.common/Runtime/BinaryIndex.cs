using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class BinaryIndex<V, K> where K : IComparable
{
    private BinaryTree<V> Root { get; set; }
    private Func<V, K> Selector { get; set; }

    public BinaryIndex(Func<V, K> selector)
    {
        Selector = selector;
    }

    public void Add(V value)
    {

    }

    public void Remove(V value)
    {

    }

    public void Recompile()
    {

    }

    public IEnumerable<V> Between(K min, K max)
    {
        var stack = new Stack<V>();

        GetBigger(Root, min, stack);
        GetLesser(Root, max, stack);

        return stack;
    }

    private void GetBigger(BinaryTree<V> tree, K min, Stack<V> stack)
    {
        if (Selector(tree.Value).CompareTo(min) == 0)
        {

        }

    }

    private void GetLesser(BinaryTree<V> tree, K max, Stack<V> stack)
    {

    }
}