using System;
using System.Linq;

namespace FubarDev.Afa.CalculationMethods
{
    /// <summary>
    /// Arithmetisch-Progressive Abschreibung
    /// </summary>
    public class ArithmeticProgressive : Arithmetic, ICalculationMethod
    {
        /// <summary>
        /// Berechnet die Gewichtung eines Abschreibungsjahres.
        /// </summary>
        /// <param name="period">Gewichtung für das Jahr (beginnt mit 1)</param>
        /// <param name="depreciationRange">Nutzungsdauer</param>
        /// <returns>Gewichtung</returns>
        public override int CalculateWeight(int period, int depreciationRange)
        {
            return period;
        }

        private decimal CalculateFactor(CalculationData data, int maxParts)
        {
            return (data.AcquisitionValue - data.TargetRemainingValue) / maxParts;
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
