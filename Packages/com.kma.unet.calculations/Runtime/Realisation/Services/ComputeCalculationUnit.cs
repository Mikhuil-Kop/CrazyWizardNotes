using System;
using UNet.Calculations.Abstractions;
using UnityEngine;

namespace UNet.Calculations.Realisation
{
    public class ComputeCalculationUnit : ICalculationUnit
    {
        #region Инициализация

        private ComputeShader ComputeShader { get; }

        public ComputeCalculationUnit(ComputeShader computeShader)
        {
            ComputeShader = computeShader;
        }

        #endregion

        #region Установка данных

        public ICalculationUnit Set(string key, bool data)
        {
            ComputeShader.SetBool(key, data);
            return this;
        }

        public ICalculationUnit Set(string key, float data)
        {
            ComputeShader.SetFloat(key, data);
            return this;
        }

        public ICalculationUnit Set(string key, Vector2D data)
        {
            ComputeShader.SetVector(key, (Vector2)data);
            return this;
        }

        public ICalculationUnit Set(string key, ICalculationData data, int actionID)
        {
            var computeData = data as ComputeCalculationData?? throw new Exception("Тип ICalculationData не совпадает с ожидаемым. Скорее всего ICalculationData и ICalculationUnit были получены из разных сервисов.");

            ComputeShader.SetBuffer(actionID, key, computeData.ComputeBuffer);

            return this;
        }

        #endregion

        #region Работа с действиями

        public ICalculationUnitAction Action(int actionID)
        {
            return new ComputeCalculationUnitAction(this, actionID);
        }

        public ICalculationUnitAction Action(string actionKey)
        {
            var actionID = ComputeShader.FindKernel(actionKey);
            return new ComputeCalculationUnitAction(this, actionID);
        }

        #endregion

        #region Выполнение вычислений

        public void Execute(int actionID, int x = 1, int y = 1, int z = 1)
        {
            var threadGroupSizes = GetThreadGroupSizes(ComputeShader, actionID);

            int numGroupsX = Mathf.CeilToInt(x / (float)threadGroupSizes.x);
            int numGroupsY = Mathf.CeilToInt(y / (float)threadGroupSizes.y);
            int numGroupsZ = Mathf.CeilToInt(z / (float)threadGroupSizes.y);

            ComputeShader.Dispatch(actionID, numGroupsX, numGroupsY, numGroupsZ);
        }

        private static Vector3Int GetThreadGroupSizes(ComputeShader compute, int kernelIndex = 0)
        {
            compute.GetKernelThreadGroupSizes(kernelIndex, out uint x, out uint y, out uint z);
            return new Vector3Int((int)x, (int)y, (int)z);
        }

        #endregion

        #region Прочее

        public void Dispose()
        {
        }

        #endregion
    }
}