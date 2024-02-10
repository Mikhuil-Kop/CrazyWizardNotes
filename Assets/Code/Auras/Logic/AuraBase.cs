using System.Linq;
using System.Threading.Tasks;

namespace CrazyWizardNotes.Auras.Logic
{
    public abstract class AuraBase<T> : IAura where T : struct
    {
        public int Height { get; private set; }
        public int Width { get; private set; }
        public T[] Values { get; private set; }


        public AuraBase(int height, int width)
        {
            Height = height;
            Width = width;
            Values = new T[Height * Width];
        }

        #region Поведение

        public async Task UpdateAsync()
        {
            var newValues = new T[Height * Width];

            var tasks = Enumerable.Range(0, 8)
                .Select(x => Task.Run(() => UpdateValues(newValues, x, 8)))
                .ToArray();

            await Task.WhenAll(tasks);

            Values = newValues;
        }

        private void UpdateValues(T[] values, int part, int divider)
        {
            var begin = (Width * Height / divider) * part;
            var end = (Width * Height / divider) * (part + 1);

            for (int i = begin; i < end; i++)
            {
                values[i] = UpdateValue(i);
            }
        }

        protected abstract T UpdateValue(int index);

        #endregion

        #region Внешний вид

        public AuraColor[] GetAuraColors()
        {
            return Values.Select((x, i) => GetColor(i)).ToArray();
        }

        protected abstract AuraColor GetColor(int index);

        #endregion

        #region Вспомогательное

        protected int Convert(int x, int y)
        {
            return x + y * Width;
        }

        protected (int x, int y) Convert(int index)
        {
            return (index % Width, index / Width);
        }

        protected T? Get(int index)
        {
            if (index < 0 || index >= Values.Length)
            {
                return null;
            }
            else
            {
                return Values[index];
            }
        }

        protected int Up => -Width;
        protected int Down => Width;
        protected int Left => -1;
        protected int Right => 1;

        #endregion
    }
}