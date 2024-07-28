using System.Collections.Generic;
using UNet.Calculations.Abstractions;
using UnityEngine;

namespace UNet.Calculations.Realisation
{
    public class ComputeCalculationsService : ICalculationsService
    {
        private LinkedList<ICalculationUnit> Units { get; }
        private LinkedList<ICalculationRenderUnit> RUnits { get; }
        private LinkedList<ICalculationData> Datas { get; }

        public ComputeCalculationsService()
        {
            Units = new LinkedList<ICalculationUnit>();
            RUnits = new LinkedList<ICalculationRenderUnit>();
            Datas = new LinkedList<ICalculationData>();
        }

        public ICalculationUnit CreateCalculationUnit(string name)
        {
            var computeShader = Resources.Load<ComputeShader>(name) ?? throw new KeyNotFoundException(name);
            var unit = new ComputeCalculationUnit(computeShader);

            Units.AddLast(unit);
            return unit;
        }

        public ICalculationRenderUnit CreateCalculationRenderUnit(string name)
        {
            var shader = Resources.Load<Shader>(name) ?? throw new KeyNotFoundException(name);
            var unit = new ComputeCalculationRenderUnit(shader);

            RUnits.AddLast(unit);
            return unit;
        }

        public ICalculationData<T> CreateCalculationData<T>(int count)
        {
            var data = new ComputeCalculationData<T>(count);

            Datas.AddLast(data);
            return data;
        }

        public ICalculationData<T> CreateCalculationData<T>(T[] values)
        {
            var data = new ComputeCalculationData<T>(values);

            Datas.AddLast(data);
            return data;
        }

        public void Dispose()
        {
            foreach (var unit in Units)
            {
                unit.Dispose();
            }

            foreach (var unit in RUnits)
            {
                unit.Dispose();
            }

            foreach (var data in Datas)
            {
                data.Dispose();
            }
        }
    }
}