using System;

namespace FubarDev.Afa.CalculationMethods
{
    /// <summary>
    /// Geometrisch-Degressive Abschreibung
    /// </summary>
    public class GeometricDegressive : ICalculationMethod
    {
        private decimal CalculateFactor(CalculationData data)
        {
            return DecimalMath.Root(data.TargetRemainingValue / data.AcquisitionValue, data.DepreciationRange);
        }

        /// <summary>
        /// Berechnung des Abschreibungs- und Restbuchwertes (<see cref="CalculationResult"/>) für das Abschreibungsjahr (<paramref name="period"/>).
        /// </summary>
        /// <param name="data">Die grundlegenden Daten für die Abschreibungsberechnung</param>
        /// <param name="period">Das Abschreibungsjahr für das die Daten errechnet werden sollen.</param>
        /// <returns>Die errechneten Abschreibungs- und Restbuchwerte</returns>
        public CalculationResult CalculateDepreciation(CalculationData data, int period)
        {
            if (period < 1 || period > data.DepreciationRange)
                throw new ArgumentOutOfRangeException(nameof(period), "The period must be greater than 0 and less than the value of depreciationRange.");

            var factor = CalculateFactor(data);

            var factorForOldValue = DecimalMath.Pow(factor, period - 1);
            var oldValue = data.AcquisitionValue * factorForOldValue;
            var remainingValue = factor * oldValue;
            var depreciation = oldValue - remainingValue;

            return new CalculationResult(period, depreciation, remainingValue);
        }
    }
}
