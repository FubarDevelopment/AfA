namespace FubarDev.Afa.Entities
{
    /// <summary>
    /// Berechnungsverfahren
    /// </summary>
    public enum AfaCalculationMethod
    {
        /// <summary>
        /// Linear
        /// </summary>
        Linear,

        /// <summary>
        /// Geometrisch degressiv
        /// </summary>
        GeometricDegressive,

        /// <summary>
        /// Geometrisch progressiv
        /// </summary>
        GeometricProgressive,

        /// <summary>
        /// Arithmetisch degressiv
        /// </summary>
        ArithmeticDegressive,

        /// <summary>
        /// Arithmetisch progressiv
        /// </summary>
        ArithmeticProgressive,

        /// <summary>
        /// Geringwertiges Wirtschaftsgut
        /// </summary>
        LowValueFixedAsset,

        /// <summary>
        /// Prozentual zu Linear
        /// </summary>
        PercentToLinear,
    }
}
