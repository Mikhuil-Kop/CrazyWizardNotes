using System;

namespace UNet.Calculations.Abstractions
{
    public interface ICalculationUnit : IDisposable
    {
        void Execute(int actionID, int x = 1, int y = 1, int z = 1);

        ICalculationUnit Set(string key, bool data);
        ICalculationUnit Set(string key, float data);
        ICalculationUnit Set(string key, Vector2D vector);
        ICalculationUnit Set(string key, ICalculationData data, int actionID);

        ICalculationUnitAction Action(int actionID);
        ICalculationUnitAction Action(string actionKey);

    }
}