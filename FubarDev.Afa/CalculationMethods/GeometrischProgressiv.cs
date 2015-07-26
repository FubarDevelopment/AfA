using System;
using System.Linq;

namespace FubarDev.Afa.CalculationMethods
{
    public class GeometrischProgressiv : ICalculationMethod
    {
        private decimal CalculateFactor(CalculationData data)
        {
            return DecimalMath.Root(data.TargetRemainingValue / data.AcquisitionValue, data.DepreciationRange);
        }

        public CalculationResult CalculateDepreciation(CalculationData data, int period)
        {
            if (period < 1 || period > data.DepreciationRange)
                throw new ArgumentOutOfRangeException(nameof(period), "The period must be greater than 0 and less than the value of depreciationRange.");

            var factor = CalculateFactor(data);

            var depreciations =
                Enumerable.Range(0, 6)
                .Select(x => DecimalMath.Pow(factor, x))
                .Select(x => data.AcquisitionValue * x)
                .SelectWithPrevious((prev, current) => prev - current)
                .Reverse()
                .ToList();

            var depreciation = depreciations[period - 1];
            var remainingValue = data.AcquisitionValue - depreciations.Take(period).Sum();

            return new CalculationResult(period, depreciation, remainingValue);
        }
    }
}
