namespace FubarDev.Afa.CalculationMethods
{
    /// <summary>
    /// Basis-Klasse für arithmetische Abschreibung
    /// </summary>
    public abstract class Arithmetic
    {
        /// <summary>
        /// Berechnet die Gewichtung eines Abschreibungsjahres.
        /// </summary>
        /// <param name="period">Gewichtung für das Jahr (beginnt mit 1)</param>
        /// <param name="depreciationRange">Nutzungsdauer</param>
        /// <returns>Gewichtung</returns>
        public abstract int CalculateWeight(int period, int depreciationRange);
    }
}
