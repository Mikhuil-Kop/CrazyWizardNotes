using System;
using UNet.Calculations.Abstractions;

namespace UNet.Liquids
{
    public class LiquidSort
    {
        #region Сервисы

        private ICalculationsService CalculationsService { get; }

        #endregion

        #region Вычисления

        private ICalculationUnit Unit { get; set; }
        private ICalculationData Indexes { get; set; }
        private ICalculationData Offsets { get; set; }
        private ICalculationUnitAction SortAction { get; set; }
        private ICalculationUnitAction OffsetAction { get; set; }

        #endregion

        #region Функция

        public LiquidSort(ICalculationsService calculationsService)
        {
            CalculationsService = calculationsService;
        }

        public void SetBuffers(ICalculationData indexes, ICalculationData offsets)
        {
            if (indexes.Count != offsets.Count)
            {
                throw new ArgumentException("indexes.Count must be equal to offsets.Count.");
            }

            Indexes = indexes;
            Offsets = offsets;

            Unit = CalculationsService.CreateCalculationUnit("Shaders/BitonicMergeSort");

            SortAction = Unit.Action("Sort")
                .Set("Entries", indexes)
                .Set("Offsets", offsets);

            OffsetAction = Unit.Action("CalculateOffsets")
                .Set("Entries", indexes)
                .Set("Offsets", offsets);
        }

        public void Sort()
        {
            Unit.Set("numEntries", Indexes.Count);

            var numStages = (int)Math.Log(NextPowerOfTwo(Indexes.Count), 2);

            for (var stageIndex = 0; stageIndex < numStages; stageIndex++)
            {
                for (var stepIndex = 0; stepIndex < stageIndex + 1; stepIndex++)
                {
                    var groupWidth = 1 << (stageIndex - stepIndex);
                    var groupHeight = 2 * groupWidth - 1;

                    Unit.Set("groupWidth", groupWidth);
                    Unit.Set("groupHeight", groupHeight);
                    Unit.Set("stepIndex", stepIndex);

                    SortAction.Execute(NextPowerOfTwo(Indexes.Count) / 2);
                }
            }
        }

        public void SortAndCalculateOffsets()
        {
            Sort();

            OffsetAction.Execute(Indexes.Count);
        }

        private static int NextPowerOfTwo(int value)
        {
            value--;
            value |= value >> 16;
            value |= value >> 8;
            value |= value >> 4;
            value |= value >> 2;
            value |= value >> 1;
            return value + 1;
        }

        #endregion
    }
}
