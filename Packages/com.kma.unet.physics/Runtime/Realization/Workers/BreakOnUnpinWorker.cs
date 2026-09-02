using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;

namespace UNet.Physics.Realization
{
    public class BreakOnUnpinWorker : IHandlesStart
    {
        private IPinable Pinable { get; }
        private IBreakable Breakable { get; }

        public BreakOnUnpinWorker(IPinable pinable)
        {
            Pinable = pinable;
        }

        public void OnStart()
        {
            Pinable.OnUnpin += Pinable_OnUnpin;
        }

        private void Pinable_OnUnpin()
        {
            Breakable.Break();
        }
    }
}
