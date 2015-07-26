using System;
using System.Linq;

namespace FubarDev.Afa.CalculationMethods
{
    public class ArithmetischProgressiv : ICalculationMethod
    {
        public ArithmetischProgressiv()
            : this((period, range) => period)
        {

        }

        public ArithmetischProgressiv(WeightCalculationDelegate weightCalculator)
        {
            CalculateWeight = weightCalculator;
        }

        public WeightCalculationDelegate CalculateWeight { get; }

        private decimal CalculateFactor(CalculationData data, int maxParts)
        {
            return (data.AcquisitionValue - data.TargetRemainingValue) / maxParts;
        }

        public CalculationResult CalculateDepreciation(CalculationData data, int period)
        {
            if (period < 1 || period > data.DepreciationRange)
                throw new ArgumentOutOfRangeException(nameof(period), "The period must be greater than 0 and less than the value of depreciationRange.");

            var deprecationParts = CalculateWeight(period, data.DepreciationRange);
            var previousParts = Enumerable.Range(1, period)
                .Select(x => CalculateWeight(x, data.DepreciationRange))
                .Sum();
            var maxParts = previousParts + Enumerable.Range(1 + period, data.DepreciationRange - period)
                .Select(x => CalculateWeight(x, data.DepreciationRange))
                .Sum();
            var amount = CalculateFactor(data, maxParts);

            var depreciation = amount * deprecationParts;
            var remainingValue = data.AcquisitionValue - amount * previousParts;

            return new CalculationResult(period, depreciation, remainingValue);
        }
    }
}
