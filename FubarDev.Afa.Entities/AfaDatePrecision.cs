namespace FubarDev.Afa.Entities
{
    /// <summary>
    /// Genauigkeit des AfA-Datums
    /// </summary>
    public enum AfaDatePrecision
    {
        /// <summary>
        /// 30 Tage pro Monat
        /// </summary>
        ThirtyDays,

        /// <summary>
        /// Genaue Anzahl Tage pro Monat
        /// </summary>
        ActualDays,
    }
}
