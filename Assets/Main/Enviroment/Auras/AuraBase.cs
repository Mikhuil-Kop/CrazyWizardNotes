using System.Threading.Tasks;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public interface IAura
{
    Task UpdateAsync();
    Color32[] GetColors();
}

public abstract class AuraBase<T> : IAura
{
    public T[] Values { get; set; }
    public int Size { get; set; }

    protected AuraBase(int size)
    {
        Size = size;
        Values = new T[size * size];
    }

    public async Task UpdateAsync()
    {
        int tasksCount = 8;
        int step = Size * Size / tasksCount;
        var tasks = new Stack<Task>();

        for (int i = 0; i < tasksCount; i++)
        {
            tasks.Push(UpdateSectorAsync(GetSector(i * step, (i + 1) * step)));
        }

        await Task.WhenAll();

        //Values = tasks.SelectMany(x => x.Value);
    }

    protected IEnumerable<AuraPoint> GetSector(int start, int size)
    {
        for (int i = start; i < start + size; i++)
        {
            yield return new AuraPoint(this, i);
        }
    }

    protected abstract Task UpdateSectorAsync(IEnumerable<AuraPoint> points);

    public abstract Color32[] GetColors();

    protected class AuraPoint
    {
        public AuraBase<T> Aura { get; set; }
        public T Value { get; set; }
        public int Index { get; set; }

        public int X => Index / Aura.Size;
        public int Y => Index % Aura.Size;

        public AuraPoint(AuraBase<T> aura, int index)
        {
            Aura = aura;
            Index = index;
            Value = aura.Values[index];
        }
    }
}

public class LiquidAura : AuraBase<byte>
{
    public LiquidAura() : base(200)
    {
    }

    public override Color32[] GetColors()
    {
        return Values.Select(x => new Color32(x, 0, 0, 255)).ToArray();
    }

    protected async override Task UpdateSectorAsync(IEnumerable<AuraPoint> points)
    {
        var result = await Task.Run(() => points.ToList());
    }
}
