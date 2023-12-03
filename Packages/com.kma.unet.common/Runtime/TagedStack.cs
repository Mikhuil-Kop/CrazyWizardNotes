using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TagedStack<V, K> : IEnumerable<TaggedValue<V, K>>
{
    private Stack<TagedStackNode> Nodes;


    public TagedStack()
    {
        Nodes = new Stack<TagedStackNode>();
    }

    public void Add(V value, params K[] keys)
    {
        var stackNode = Nodes.FirstOrDefault(x => x.Keys.Length == keys.Length && x.Keys.All(x => keys.Contains(x)));

        if (stackNode == null)
        {
            stackNode = new TagedStackNode()
            {
                Keys = keys,
                Values = new Stack<V>()
            };

            Nodes.Push(stackNode);
        }

        stackNode.Values.Push(value);
    }

    public void AddRange(IEnumerable<V> values, params K[] keys)
    {
        var stackNode = Nodes.FirstOrDefault(x => x.Keys.Length == keys.Length && x.Keys.All(x => keys.Contains(x)));

        if (stackNode == null)
        {
            stackNode = new TagedStackNode()
            {
                Keys = keys,
                Values = new Stack<V>()
            };

            Nodes.Push(stackNode);
        }

        foreach (var value in values)
        {
            stackNode.Values.Push(value);
        }
    }

    public IEnumerable<TaggedValue<V, K>> WhereAny(params K[] keys)
    {
        var nodes = Nodes.Where(x => x.Keys.Length == keys.Length && x.Keys.All(x => keys.Contains(x)));

        foreach (var node in nodes)
        {
            foreach (var val in node.Values)
            {
                yield return new TaggedValue<V, K>()
                {
                    Keys = node.Keys,
                    Values = val
                };
            }
        }
    }

    public IEnumerable<TaggedValue<V, K>> WhereAll(params K[] keys)
    {
        var node = Nodes.FirstOrDefault(x => x.Keys.Length == keys.Length && x.Keys.All(x => keys.Contains(x)));

        foreach (var val in node.Values)
        {
            yield return new TaggedValue<V, K>()
            {
                Keys = node.Keys,
                Values = val
            };
        }
    }


    public IEnumerator<TaggedValue<V, K>> GetEnumerator()
    {
        foreach (var node in Nodes)
        {
            foreach (var val in node.Values)
            {
                yield return new TaggedValue<V, K>()
                {
                    Keys = node.Keys,
                    Values = val
                };
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }


    private class TagedStackNode
    {
        public K[] Keys;
        public Stack<V> Values;
    }
}


public class TaggedValue<V, K>
{
    public K[] Keys;
    public V Values;
}


public static class TagedStackExtension
{
    public static IEnumerable<TaggedValue<V, K>> WhereAny<V, K>(this IEnumerable<TaggedValue<V, K>> enumarable, params K[] keys)
    {
        return enumarable.Where(x => x.Keys.Any(x => keys.Contains(x)));
    }

    public static IEnumerable<TaggedValue<V, K>> WhereAll<V, K>(this IEnumerable<TaggedValue<V, K>> enumarable, params K[] keys)
    {
        return enumarable.Where(x => x.Keys.Length == keys.Length && x.Keys.All(x => keys.Contains(x)));
    }
}