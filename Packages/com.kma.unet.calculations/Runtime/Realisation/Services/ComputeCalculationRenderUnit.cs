using System;
using UNet.Calculations.Abstractions;
using UnityEngine;

namespace UNet.Calculations.Realisation
{
    public class ComputeCalculationRenderUnit : ICalculationRenderUnit
    {
        #region Инициализация

        private Shader Shader { get; }
        private Material Material { get; }

        public ComputeCalculationRenderUnit(Shader shader)
        {
            Shader = shader;
            Material = new Material(shader);
        }

        #endregion

        #region Установка данных

        public ICalculationRenderUnit Set(string key, bool data)
        {
            Material.SetInteger(key, data ? 1 : 0);
            return this;
        }

        public ICalculationRenderUnit Set(string key, float data)
        {
            Material.SetFloat(key, data);
            return this;
        }

        public ICalculationRenderUnit Set(string key, Vector2D data)
        {
            Material.SetVector(key, (Vector2)data);
            return this;
        }

        public ICalculationRenderUnit Set(string key, ICalculationData data)
        {
            var computeData = data as ComputeCalculationData ?? throw new Exception("Тип ICalculationData не совпадает с ожидаемым. Скорее всего ICalculationData и ICalculationUnit были получены из разных сервисов.");

            Material.SetBuffer(key, computeData.ComputeBuffer);
            return this;
        }

        #endregion

        #region Прочее

        public Material GetMaterial()
        {
            return Material;
        }

        public void Dispose()
        {
        }

        #endregion
    }
}