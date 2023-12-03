using System;
using System.Collections.Generic;
using System.Linq;
using UNet;

public static class LinqExtension
{
    public static void DisposeAll<T>(this IEnumerable<T> enumarable)
    {
        if (enumarable == null) throw new ArgumentNullException(nameof(enumarable));

        foreach (var item in enumarable)
        {
            if (item != null && item is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }

    public static Vector2D Sum<T>(this IEnumerable<T> enumarable, Func<T, Vector2D> func)
    {
        if (enumarable == null) throw new ArgumentNullException(nameof(enumarable));
        if (func == null) throw new ArgumentNullException(nameof(func));

        return enumarable.Aggregate(Vector2D.Zero, (x, y) => x + func(y));
    }

    public static Vector3D Sum<T>(this IEnumerable<T> enumarable, Func<T, Vector3D> func)
    {
        if (enumarable == null) throw new ArgumentNullException(nameof(enumarable));
        if (func == null) throw new ArgumentNullException(nameof(func));

        return enumarable.Aggregate(Vector3D.Zero, (x, y) => x + func(y));
    }

    public static Vector4D Sum<T>(this IEnumerable<T> enumarable, Func<T, Vector4D> func)
    {
        if (enumarable == null) throw new ArgumentNullException(nameof(enumarable));
        if (func == null) throw new ArgumentNullException(nameof(func));

        return enumarable.Aggregate(Vector4D.Zero, (x, y) => x + func(y));
    }

    public static Vector2D Sum(this IEnumerable<Vector2D> enumarable)
    {
        if (enumarable == null) throw new ArgumentNullException(nameof(enumarable));

        return enumarable.Aggregate(Vector2D.Zero, (x, y) => x + y);
    }

    public static Vector3D Sum(this IEnumerable<Vector3D> enumarable)
    {
        if (enumarable == null) throw new ArgumentNullException(nameof(enumarable));

        return enumarable.Aggregate(Vector3D.Zero, (x, y) => x + y);
    }

    public static Vector4D Sum(this IEnumerable<Vector4D> enumarable)
    {
        if (enumarable == null) throw new ArgumentNullException(nameof(enumarable));

        return enumarable.Aggregate(Vector4D.Zero, (x, y) => x + y);
    }

    public static IEnumerable<LinkedListNode<T>> AsNodes<T>(this LinkedList<T> list)
    {
        if (list == null) throw new ArgumentNullException(nameof(list));

        for (var node = list.First; node != null; node = node.Next)
        {
            yield return node;
        }
    }
    // ---------- MaxBy ----------

    public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey> comparer = null)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (selector == null) throw new ArgumentNullException(nameof(selector));

        if (!TryMaxBy(source, selector, comparer, out var result))
            throw new InvalidOperationException("Sequence contains no elements");

        return result;
    }

    public static TSource MaxByOrDefault<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey> comparer = null)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (selector == null) throw new ArgumentNullException(nameof(selector));

        TryMaxBy(source, selector, comparer, out var result);
        return result;
    }

    // ---------- MinBy ----------

    public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey> comparer = null)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (selector == null) throw new ArgumentNullException(nameof(selector));

        if (!TryMinBy(source, selector, comparer, out var result))
            throw new InvalidOperationException("Sequence contains no elements");

        return result;
    }

    public static TSource MinByOrDefault<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey> comparer = null)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (selector == null) throw new ArgumentNullException(nameof(selector));

        TryMinBy(source, selector, comparer, out var result);
        return result;
    }

    // ---------- Общая логика ----------

    private static bool TryMaxBy<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey> comparer, out TSource result)
    {
        comparer ??= Comparer<TKey>.Default;

        var hasValue = false;
        result = default;
        TKey bestKey = default;

        foreach (var element in source)
        {
            var key = selector(element);

            if (!hasValue || comparer.Compare(key, bestKey) > 0)
            {
                result = element;
                bestKey = key;
                hasValue = true;
            }
        }

        return hasValue;
    }

    private static bool TryMinBy<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey> comparer, out TSource result)
    {
        comparer ??= Comparer<TKey>.Default;

        var hasValue = false;
        result = default;
        TKey bestKey = default;

        foreach (var element in source)
        {
            var key = selector(element);

            if (!hasValue || comparer.Compare(key, bestKey) < 0)
            {
                result = element;
                bestKey = key;
                hasValue = true;
            }
        }

        return hasValue;
    }

    public static int IndexOf<T>(this IEnumerable<T> enumarable, Func<T, bool> selector)
    {
        int i = -1;

        foreach (var x in enumarable)
        {
            i++;

            if (selector(x))
            {
                break;
            }
        }

        return i;
    }

    public static IEnumerable<T> BeforeIndex<T>(this IEnumerable<T> enumarable, int index)
    {
        return enumarable.Where((x, i) => i < index);
    }

    public static IEnumerable<T> AfterIndex<T>(this IEnumerable<T> enumarable, int index)
    {
        return enumarable.Where((x, i) => i > index);
    }

    public static IEnumerable<V> As<V>(this IEnumerable<object> enumerable) where V : class
    {
        return enumerable.Select(x => x as V).Where(x => x != null);
    }

    public static IEnumerable<V> As<T, V>(this IEnumerable<T> enumerable) where T : class where V : class
    {
        return enumerable.Select(x => x as V).Where(x => x != null);
    }

    public static IEnumerable<T> ConcatAdd<T>(this IEnumerable<T> enumerable, T value)
    {
        if (value == null)
        {
            return enumerable;
        }
        else
        {
            return enumerable.Concat(new[] { value });
        }
    }

    public static LinkedList<T> ToLinkedList<T>(this IEnumerable<T> enumerable)
    {
        return new LinkedList<T>(enumerable);
    }

    public static IEnumerable<Pair<T1, T2>> ToParallel<T1, T2>(this IEnumerable<T1> col1, IEnumerable<T2> col2)
    {
        using var en1 = col1.GetEnumerator();
        using var en2 = col2.GetEnumerator();

        while (true)
        {
            yield return new Pair<T1, T2>
            {
                Item1 = en1.Current,
                Item2 = en2.Current
            };

            var b1 = en1.MoveNext();
            var b2 = en2.MoveNext();

            if (!b1 && !b2)
            {
                break;
            }
        }
    }

    public class Pair<T1, T2>
    {
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
    }

    #region Рекурсии

    public static IEnumerable<T> ToRecursion<T>(this T root, Func<T, T> selector)
    {
        var now = root;

        while (now != null)
        {
            yield return now;

            now = selector.Invoke(now);
        }
    }

    public static IEnumerable<T> ToRecursionUnique<T>(this T root, Func<T, IEnumerable<T>> selector, IEqualityComparer<T> comparer = null)
    {
        if (root == null) throw new ArgumentNullException(nameof(root));
        if (selector == null) throw new ArgumentNullException(nameof(selector));

        return ToRecursionUnique(new[] { root }, selector, comparer);
    }

    public static IEnumerable<T> ToRecursionUnique<T>(this IEnumerable<T> roots, Func<T, IEnumerable<T>> selector, IEqualityComparer<T> comparer = null)
    {
        if (roots == null) throw new ArgumentNullException(nameof(roots));
        if (selector == null) throw new ArgumentNullException(nameof(selector));

        var visited = new HashSet<T>(comparer);
        var queue = new Queue<T>();

        foreach (var root in roots)
        {
            if (visited.Add(root))
            {
                queue.Enqueue(root);
                yield return root;
            }
        }

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            var children = selector(current);

            if (children != null)
            {
                foreach (var child in children)
                {
                    if (visited.Add(child))
                    {
                        queue.Enqueue(child);
                        yield return child;
                    }
                }
            }
        }
    }

    #endregion

    #region Графы

    public static IEnumerable<GraphNode<T>> ToGraph<T>(this IEnumerable<T> roots, Func<T, IEnumerable<T>> selector, IEqualityComparer<T> comparer = null)
    {
        if (roots == null) throw new ArgumentNullException(nameof(roots));
        if (selector == null) throw new ArgumentNullException(nameof(selector));

        comparer ??= EqualityComparer<T>.Default;
        var nodes = new Dictionary<T, GraphNode<T>>(comparer);
        var visited = new HashSet<T>(comparer);
        var queue = new Queue<T>();

        foreach (var root in roots)
        {
            if (root == null || !visited.Add(root)) continue;
            queue.Enqueue(root);
            nodes[root] = new GraphNode<T> { Value = root };

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                var neighbors = selector(current);
                if (neighbors == null) continue;

                foreach (var next in neighbors)
                {
                    if (next == null) continue;
                    if (!nodes.ContainsKey(next))
                        nodes[next] = new GraphNode<T> { Value = next };
                    if (visited.Add(next))
                        queue.Enqueue(next);
                }
            }
        }

        foreach (var kv in nodes)
        {
            var list = new List<GraphNode<T>>();
            var neighbors = selector(kv.Key);

            if (neighbors != null)
            {
                foreach (var next in neighbors)
                {
                    if (next != null && nodes.TryGetValue(next, out var node))
                        list.Add(node);
                }
            }
            kv.Value.Neighbors = list.ToArray();
        }

        return nodes.Values;
    }

    public class GraphNode<T>
    {
        public T Value { get; set; }
        public GraphNode<T>[] Neighbors { get; set; }
    }

    #endregion
}