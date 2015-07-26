using System;

namespace FubarDev.Afa.CalculationMethods
{
    public class Linear : ICalculationMethod
    {
        public CalculationResult CalculateDepreciation(CalculationData data, int period)
        {
            if (period < 1 || period > data.DepreciationRange)
                throw new ArgumentOutOfRangeException(nameof(period), "The period must be greater than 0 and less than the value of depreciationRange.");

            var depreciation = (data.AcquisitionValue - data.TargetRemainingValue) / data.DepreciationRange;
            var remainingValue = data.AcquisitionValue - depreciation * period;

            return new CalculationResult(period, depreciation, remainingValue);
        }
    }
}
