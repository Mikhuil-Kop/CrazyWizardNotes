using System.Threading.Tasks;

namespace CrazyWizardNotes.Auras.Logic
{
    public class FireAura : AuraBase<FireAuraElement>
    {
        public FireAura(int height, int width) : base(height, width)
        {
            for (int i = 0; i < height * width; i++)
            {
                Values[i] = new FireAuraElement() { Temperature = 200 };
            }
        }

        protected override AuraColor GetColor(int index)
        {
            var val = Get(index);

            return new AuraColor()
            {
                R = (byte)(val.Value.Temperature + 100).Limit(0, 255),
                G = 0,
                B = 0
            };
        }

        protected override FireAuraElement UpdateValue(int index)
        {
            var val = Get(index).Value;

            val.Temperature -= 50;

            return val;
        }
    }

    public struct FireAuraElement
    {
        public short Temperature;
    }
}