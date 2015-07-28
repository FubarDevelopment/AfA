using System;
using System.Diagnostics;

namespace FubarDev.Afa.CalculationMethods
{
    /// <summary>
    /// Wechselt von der linearen in die degressive Methode, wenn der Abschreibungswert der linearen Methode den Abschreibungswert der degressiven überschreitet.
    /// </summary>
    public class DegressiveToLinear : ICalculationMethod
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="DegressiveToLinear"/> Klasse.
        /// </summary>
        /// <param name="degressiveMethod">Das degressive Verfahren, das zu Beginn zu benutzen ist</param>
        public DegressiveToLinear(IDegressiveCalculationMethod degressiveMethod)
        {
            DegressiveMethod = degressiveMethod;
            LinearMethod = new Linear();
        }

        /// <summary>
        /// Holt die degressive Methode zur Errechnung des Abschreibungswertes.
        /// </summary>
        public IDegressiveCalculationMethod DegressiveMethod { get; }

        /// <summary>
        /// Holt die lineare Methode zur Errechnung des Abschreibungswertes.
        /// </summary>
        public Linear LinearMethod { get; }

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

            var switchedToLinear = false;
            var linearYear = 1;
            CalculationData linearData = null;
            decimal bookingValue = data.AcquisitionValue;
            decimal depreciation;
            var currentPeriod = 0;
            do
            {
                currentPeriod += 1;
                if (switchedToLinear)
                {
                    Debug.Assert(linearData != null, "Kann nicht NULL sein, weil - solange der Abschreibungswert des degressiven Verfahren errechnet wird - linearData gesetzt wird.");
                    linearYear += 1;
                    var linearResult = LinearMethod.CalculateDepreciation(linearData, linearYear);
                    depreciation = linearResult.Depreciation;
                }
                else
                {
                    var degressiveResult = DegressiveMethod.CalculateDepreciation(data, currentPeriod);
                    linearData = new CalculationData(bookingValue, data.TargetRemainingValue, data.DepreciationRange - currentPeriod + 1);
                    var linearResult = LinearMethod.CalculateDepreciation(linearData, linearYear);
                    if (degressiveResult.Depreciation < linearResult.Depreciation)
                    {
                        switchedToLinear = true;
                        depreciation = linearResult.Depreciation;
                    }
                    else
                    {
                        depreciation = degressiveResult.Depreciation;
                    }
                }
                bookingValue -= depreciation;
            }
            while (currentPeriod != period);
            return new CalculationResult(period, depreciation, bookingValue);
        }
    }
}
