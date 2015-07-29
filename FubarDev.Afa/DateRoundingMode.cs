using System;

namespace FubarDev.Afa
{
    /// <summary>
    /// Art der Rundung eines AFA-Datums
    /// </summary>
    public enum DateRoundingMode
    {
        /// <summary>
        /// Auf den Tag genau
        /// </summary>
        Day,

        /// <summary>
        /// Auf den nächstgelegenen Monat
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item><code>dd &gt;= 15</code>: Nächster Monat</item>
        /// <item><code>dd &lt; 15</code>: Aktueller Monat</item>
        /// </list>
        /// </remarks>
        Month,

        /// <summary>
        /// Auf den Monatsanfang (01.MM.yyyy)
        /// </summary>
        BeginOfMonth,

        /// <summary>
        /// Auf das halbe Jahr
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item><code>MM &gt;= 7</code>: Nächstes Jahr</item>
        /// <item><code>MM &lt; 7</code>: Aktuelles Jahr</item>
        /// </list>
        /// </remarks>
        HalfYear,

        /// <summary>
        /// Auf den Anfang des Jahres (01.01.yyyy)
        /// </summary>
        BeginOfYear,
    }
}
