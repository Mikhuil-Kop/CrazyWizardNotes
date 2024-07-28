using UNet.Calculations.Abstractions;

namespace UNet.Calculations.Realisation
{
    public class ComputeCalculationUnitAction : ICalculationUnitAction
    {
        private ICalculationUnit Parent { get; }
        private int KernelID { get; }

        public ComputeCalculationUnitAction(ICalculationUnit parent, int kernelID)
        {
            Parent = parent;
            KernelID = kernelID;
        }

        public ICalculationUnitAction Set(string key, ICalculationData data)
        {
            Parent.Set(key, data, KernelID);

            return this;
        }

        public void Execute(int x = 1, int y = 1, int z = 1)
        {
            Parent.Execute(KernelID, x, y, z);
        }
    }
}