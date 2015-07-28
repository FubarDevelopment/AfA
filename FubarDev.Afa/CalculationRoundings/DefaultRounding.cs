using System;

namespace FubarDev.Afa.CalculationRoundings
{
    /// <summary>
    /// Standard Rundungsverfahren (Verwendung von <see cref="Math.Round(decimal,int)"/>)
    /// </summary>
    public class DefaultRounding : ICalculationRounding
    {
        private readonly int _decimals;

        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="DefaultRounding"/> Klasse.
        /// </summary>
        /// <param name="decimals">Die Anzahl der Nachkommastellen</param>
        public DefaultRounding(int decimals = 0)
        {
            _decimals = decimals;
        }

        /// <summary>
        /// Berechnet den Abschreibungs- und Restbuchwert und rundet das Ergebnis entsprechend der Implementation.
        /// </summary>
        /// <param name="method">Das Verfahren für die Abschreibungsberechnung</param>
        /// <param name="data">Die Basis-Daten anhand derer der Abschreibungswert berechnet wird.</param>
        /// <param name="period">Das Abrechnungsjahr (beginnend bei 0 = keine Abschreibung)</param>
        /// <returns>Das Ergebnis der Berechnung und Rundung</returns>
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
