namespace FubarDev.Afa.Entities
{
    /// <summary>
    /// Status der Anlage
    /// </summary>
    public enum AssetStatus
    {
        /// <summary>
        /// Kein Zugang (Vor Anschaffungsdatum)
        /// </summary>
        NotAcquired,

        /// <summary>
        /// Zugangsdatum
        /// </summary>
        Acquiring,

        /// <summary>
        /// Zugang und Abgang im gleichen Monat
        /// </summary>
        AcquiringAndDispatching,

        /// <summary>
        /// Normal (zwischen Zugang und Abgang)
        /// </summary>
        Acquired,

        /// <summary>
        /// Abgang
        /// </summary>
        Dispatching,

        /// <summary>
        /// Abgegangen (nach Abgang)
        /// </summary>
        Dispatched,
    }
}
