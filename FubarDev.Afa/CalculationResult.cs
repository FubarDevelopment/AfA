using System;

namespace FubarDev.Afa
{
    /// <summary>
    /// Das Ergebnis einer Berechnung für ein Abschreibungsjahr
    /// </summary>
    public class CalculationResult
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="CalculationResult"/> Klasse.
        /// </summary>
        /// <param name="period">Das Abschreibungsjahr (das erste Jahr ist 0)</param>
        /// <param name="depreciation">Der Abschreibungsbetrag</param>
        /// <param name="remainingValue">Der Restbuchwert nach Abzug des Abschreibungsbetrages (<paramref name="depreciation"/>)</param>
        public CalculationResult(int period, decimal depreciation, decimal remainingValue)
        {
            Period = period;
            Depreciation = depreciation;
            RemainingValue = remainingValue;
        }

        /// <summary>
        /// Das Abschreibungsjahr (beginnend bei 0)
        /// </summary>
        public int Period { get; private set; }

        /// <summary>
        /// Der Abschreibungswert
        /// </summary>
        public decimal Depreciation { get; private set; }

        /// <summary>
        /// Der Restbuchwert
        /// </summary>
        public decimal RemainingValue { get; private set; }
    }
}
