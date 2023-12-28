using System;
using System.Linq;
using System.Collections.Generic;

public static class LinqExtension
{
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

    // TODO: Удалить
    //public static IEnumerable<V> As<V, T>(this IEnumerable<T> enumerable) where V : class
    //{
    //    return enumerable.Select(x => x as V).Where(x => x != null);
    //}

    public static IEnumerable<V> As<V>(this IEnumerable<object> enumerable) where V : class
    {
        return enumerable.Select(x => x as V).Where(x => x != null);
    }
}

