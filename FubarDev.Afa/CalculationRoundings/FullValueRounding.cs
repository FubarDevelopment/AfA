using System;

namespace FubarDev.Afa.CalculationRoundings
{
    public class FullValueRounding : ICalculationRounding
    {
        private readonly int _decimals;

        public FullValueRounding(int decimals = 0)
        {
            _decimals = decimals;
        }

        public CalculationResult Calculate(ICalculationMethod method, CalculationData data, int period)
        {
            if (period < 0 || period > data.DepreciationRange)
                throw new ArgumentOutOfRangeException(nameof(period), "The period must be greater or equal than 0 and less than the value of depreciationRange.");

            if (period == 0)
                return new CalculationResult(period, 0, data.AcquisitionValue);

            var result = method.CalculateDepreciation(data, period);

            CalculationResult resultOld;
            if (period == 1)
            {
                // Nicht jede Berechnungsart funktioniert für period == 0!
                resultOld = new CalculationResult(0, 0, data.AcquisitionValue);
            }
            else
            {
                resultOld = method.CalculateDepreciation(data, period - 1);
            }

            var remainingValue = Math.Round(result.RemainingValue, _decimals);
            var remainingValueOld = Math.Round(resultOld.RemainingValue, _decimals);

            return new CalculationResult(period, remainingValueOld - remainingValue, remainingValue);
        }
    }
}
