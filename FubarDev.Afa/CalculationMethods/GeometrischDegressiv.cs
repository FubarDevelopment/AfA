using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FubarDev.Afa.CalculationMethods
{
    public class GeometrischDegressiv : ICalculationMethod
    {
        private decimal CalculateFactor(CalculationData data)
        {
            return Math.Round(DecimalMath.Root(data.TargetRemainingValue / data.AcquisitionValue, data.DepreciationRange), 4, MidpointRounding.AwayFromZero);
        }

        public CalculationResult CalculateDepreciation(CalculationData data, int period)
        {
            if (period < 1 || period > data.DepreciationRange)
                throw new ArgumentOutOfRangeException("period", "The period must be greater than 0 and less than the value of depreciationRange.");

            var factor = CalculateFactor(data);

            var factorForOldValue = DecimalMath.Pow(factor, period - 1);
            var oldValue = data.AcquisitionValue * factorForOldValue;
            var remainingValue = factor * oldValue;
            var depreciation = oldValue - remainingValue;

            return new CalculationResult(period, depreciation, remainingValue);
        }
    }
}
