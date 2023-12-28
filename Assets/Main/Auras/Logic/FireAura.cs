using System.Threading.Tasks;

namespace CrazyWizardNotes.Auras.Logic
{
    public class FireAura : AuraBase<FireAuraElement>
    {
        public FireAura(int height, int width) : base(height, width)
        {
        }

        protected override AuraColor GetColor(int index)
        {
            var val = Get(index);

            return new AuraColor()
            {
                R = 0,
                G = val.Value.Fire,
                B = val.Value.Fire
            };
        }

        protected override FireAuraElement UpdateValue(int index)
        {
            return new FireAuraElement()
            {
                Fire = 255
            };

            var val = Get(index).Value;

            if (val.Fire == 0)
            {
                val.Fire = 255;
                return val;
            }
            else
            {
                val.Fire--;
                return val;
            }
        }
    }

    public struct FireAuraElement
    {
        public ushort Joules;
        public ushort Temperature;
        public byte Fire;
        public bool CanBurn;
    }
}