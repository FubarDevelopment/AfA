using System;

namespace FubarDev.Afa.CalculationMethods
{
    /// <summary>
    /// Berechnung des Abschreibungswertes anhand einer Prozent-Zahl basierend auf dem Restbuchwert.
    /// </summary>
    public class PercentDegressive : IDegressiveCalculationMethod
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="PercentDegressive"/> Klasse.
        /// </summary>
        /// <param name="percent">Der Prozentsatz der vom Restbuchwert abzuziehen ist.</param>
        public PercentDegressive(decimal percent)
        {
            if (percent < 0 || percent > 1)
                throw new ArgumentOutOfRangeException(nameof(percent), "Der Prozent-Wert muss um Bereich von >=0 und <=1 sein.");
            Percent = percent;
        }

        /// <summary>
        /// Holt die Prozente, die zur Errechnung des Abschreibungswertes anhand des Restbuchwertes zu nutzen sind.
        /// </summary>
        public decimal Percent { get; }

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

            var bookingValue = data.AcquisitionValue - data.TargetRemainingValue;
            decimal depreciation;
            var currentPeriod = 0;
            do
            {
                currentPeriod += 1;
                depreciation = bookingValue * Percent;
                bookingValue -= depreciation;
            }
            while (currentPeriod != period);
            return new CalculationResult(period, depreciation, bookingValue);
        }
    }
}
