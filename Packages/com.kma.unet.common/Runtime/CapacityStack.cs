using System.Collections;
using System.Collections.Generic;

public class CapacityStack<T> : IEnumerable<T>
{
	public int Capacity { get; private set; }
	public int Count { get; private set; }

	private T[] Values { get; set; }
    private int FirstIndex { get; set; }

    public T First
    {
        get
        {
            return Values[FirstIndex];
        }
        set
        {
            Values[FirstIndex] = value;
        }
    }

    public CapacityStack(int capacity)
	{
        Capacity = capacity;
        Count = 0;
        FirstIndex = 0;
        Values = new T[capacity];
	}

    public void Push(T value)
    {
        if (FirstIndex == Capacity - 1)
        {
            FirstIndex = 0;
        }
        else
        {
            FirstIndex++;
        }

        if (Count != Capacity)
        {
            Count++;
        }

        Values[FirstIndex] = value;
    }

    public void Clear()
    {
        FirstIndex = 0;
        Count = 0;
        Values = new T[Capacity];
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = FirstIndex; i > 0; i--)
        {
            yield return Values[i];
        }

        if (Count == Capacity)
        {
            for (int i = Count - 1; i > FirstIndex; i--)
            {
                yield return Values[i];
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
