using System;

namespace FubarDev.Afa.CalculationMethods
{
    /// <summary>
    /// Arithmetisch-Degressive Abschreibung
    /// </summary>
    public class ArithmeticDegressive : ArithmeticProgressive
    {
        /// <summary>
        /// Berechnet die Gewichtung eines Abschreibungsjahres.
        /// </summary>
        /// <param name="period">Gewichtung für das Jahr (beginnt mit 1)</param>
        /// <param name="depreciationRange">Nutzungsdauer</param>
        /// <returns>Gewichtung</returns>
        public override int CalculateWeight(int period, int depreciationRange)
        {
            return depreciationRange - period + 1;
        }
    }
}
