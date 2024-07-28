using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BinaryTree<T> : IEnumerable<T>
{
    public BinaryTree<T> Left { get; private set; }
    public BinaryTree<T> Right { get; private set; }
    public T Value { get; set; }

    public BinaryTree(T value, BinaryTree<T> left, BinaryTree<T> right)
    {
        Left = left;
        Right = right;
        Value = value;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new T[] { Value }
            .Concat(Left ?? new T[0] as IEnumerable<T>)
            .Concat(Right ?? new T[0] as IEnumerable<T>)
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return new T[] { Value }
            .Concat(Left ?? new T[0] as IEnumerable<T>)
            .Concat(Right ?? new T[0] as IEnumerable<T>)
            .GetEnumerator();
    }
}
