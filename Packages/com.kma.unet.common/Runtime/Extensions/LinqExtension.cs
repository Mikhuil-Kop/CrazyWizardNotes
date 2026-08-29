using System;
using System.Collections.Generic;
using System.Linq;
using UNet;

public static class LinqExtension
{
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

    public static IEnumerable<V> SelectInContext<T, V>(this IEnumerable<T> enumarable, Func<T, T, V> selector)
	{
		bool initialized = false;
		T previous = default;

		foreach (var t in enumarable)
		{
			if (!initialized)
			{
				initialized = true;
			}
			else
			{
				yield return selector(previous, t);
			}

            previous = t;
		}
    }

    public static IEnumerable<V> SelectInContext<T, V>(this IEnumerable<T> enumarable, Func<T, T, T, V> selector)
    {
        int initialized = 0;
        T previous1 = default;
        T previous2 = default;

        foreach (var t in enumarable)
        {
            if (initialized <= 1)
            {
                previous1 = t;
                initialized++;
            }
            else
            {
                yield return selector(previous2, previous1, t);
            }

            previous2 = previous1;
            previous1 = t;
        }
    }

    public static IEnumerable<T> WhereInContext<T>(this IEnumerable<T> enumarable, Func<T, T, T, bool> selector)
    {
        int initialized = 0;
        T previous1 = default;
        T previous2 = default;

        foreach (var t in enumarable)
        {
            if (initialized < 2)
            {
                previous1 = t;
                initialized++;
                yield return previous1;
            }
            else
            {
                if (selector(previous2, previous1, t))
                {
                    yield return previous1;
                }
            }

            previous2 = previous1;
            previous1 = t;
        }
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
}