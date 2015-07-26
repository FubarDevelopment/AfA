using System;

namespace FubarDev.Afa.CalculationMethods
{
    /// <summary>
    /// Berechnet die Gewichtung einer Periode.
    /// </summary>
    /// <param name="period">Gewichtung für das Jahr (beginnt mit 1)</param>
    /// <param name="depreciationRange">Nutzungsdauer</param>
    /// <returns>Gewichtung</returns>
    public delegate int WeightCalculationDelegate(int period, int depreciationRange);
}
