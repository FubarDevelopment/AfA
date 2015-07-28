using System;

namespace FubarDev.Afa
{
    /// <summary>
    /// Die Grundlegenden Daten für eine Berechnung durch <see cref="ICalculationMethod"/>
    /// </summary>
    public class CalculationData
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="CalculationData"/> Klasse.
        /// </summary>
        /// <param name="acquisitionValue">Anschaffungswert</param>
        /// <param name="targetRemainingValue">Restbuchwert</param>
        /// <param name="depreciationRange">Nutzungsdauer</param>
        public CalculationData(decimal acquisitionValue, decimal targetRemainingValue, int depreciationRange)
        {
            if (targetRemainingValue >= acquisitionValue)
                throw new ArgumentOutOfRangeException(nameof(targetRemainingValue), "The target remaining value must be less than the value of acquisitionValue.");
            if (depreciationRange < 1)
                throw new ArgumentOutOfRangeException(nameof(depreciationRange), "The depreciation range must be greater than 0.");

            AcquisitionValue = acquisitionValue;
            TargetRemainingValue = targetRemainingValue;
            DepreciationRange = depreciationRange;
        }

        /// <summary>
        /// Der Anschaffungswert
        /// </summary>
        public decimal AcquisitionValue { get; }

        /// <summary>
        /// Der Restwert nach Ende der Nutzung
        /// </summary>
        public decimal TargetRemainingValue { get; }

        /// <summary>
        /// Die geplante Nutzungsdauer
        /// </summary>
        public int DepreciationRange { get; }
    }
}
