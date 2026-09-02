using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;

namespace UNet.Physics.Realization
{
    public class PinOnStartWorker : IHandlesStart
    {
        private IPinable Pinable { get; }

        public PinOnStartWorker(IPinable pinable)
        {
            Pinable = pinable;
        }

        public void OnStart()
        {
            Pinable.Pin(100f);
        }
    }
}
