using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FubarDev.Afa.CalculationMethods
{
    public class ArithmetischDegressiv : ICalculationMethod
    {
        private static int CalculateParts(int n)
        {
            return (n * (n + 1)) / 2;
        }

        private decimal CalculateFactor(CalculationData data)
        {
            return (data.AcquisitionValue - data.TargetRemainingValue) / CalculateParts(data.DepreciationRange);
        }

        public CalculationResult CalculateDepreciation(CalculationData data, int period)
        {
            if (period < 1 || period > data.DepreciationRange)
                throw new ArgumentOutOfRangeException("period", "The period must be greater than 0 and less than the value of depreciationRange.");

            var amount = CalculateFactor(data);

            var maxParts = CalculateParts(data.DepreciationRange);
            var periodParts = CalculateParts(data.DepreciationRange - period);

            var depreciation = amount * (data.DepreciationRange - period + 1);
            var remainingValue = data.AcquisitionValue - amount * (maxParts - periodParts);

            return new CalculationResult(period, depreciation, remainingValue);
        }
    }
}
