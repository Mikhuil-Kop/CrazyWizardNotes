namespace UNet.Calculations.Abstractions
{
    public interface ICalculationUnitAction
    {
        void Execute(int x = 1, int y = 1, int z = 1);
        ICalculationUnitAction Set(string key, ICalculationData data);
    }
}