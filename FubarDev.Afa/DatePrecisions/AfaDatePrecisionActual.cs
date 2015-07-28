using System;

using NodaTime;

namespace FubarDev.Afa.DatePrecisions
{
    /// <summary>
    /// AfA-Datum-Genauigkeit, die  wirklich auf den Tag genau ist (inkl. Schaltjahr, etc.).
    /// </summary>
    public class AfaDatePrecisionActual : IAfaDatePrecisionHandler
    {
        private static readonly LocalDate _startDate = new LocalDate(1753, 1, 1);

        /// <summary>
        /// Standard-Instanz für die <see cref="AfaDatePrecisionActual"/> Klasse.
        /// </summary>
        public static AfaDatePrecisionActual Default { get; } = new AfaDatePrecisionActual();

        /// <summary>
        /// Ermittelt die Anzahl an Tagen für das angegeben Jahr
        /// </summary>
        /// <param name="year">Das Jahr für das die Anzahl an Tagen ermittelt werden soll</param>
        /// <returns>Die Anzahl an Tagen für das Jahr</returns>
        public int GetTotalDaysOfYear(int year)
        {
            return new LocalDate(year, 12, 31).DayOfYear;
        }

        /// <summary>
        /// Addiert zu einem Datum (unter Berücksichtigung der geforderten Genauigkeit) die Anzahl an Jahren, Monaten und/oder Tagen.
        /// </summary>
        /// <param name="date">Das Datum zu dem die Jahre, Monate und Tage addiert werden sollen</param>
        /// <param name="addYears">Die Anzahl an Jahren die addiert werden sollen (kann auch negativ sein)</param>
        /// <param name="addMonths">Die Anzahl an Monaten die addiert werden sollen (kann auch negativ sein)</param>
        /// <param name="addDays">Die Anzahl an Tagen die addiert werden sollen (kann auch negativ sein)</param>
        /// <returns>Das neue Datum nach der Addition von Jahren, Monaten und/oder Tagen</returns>
        public LocalDate Add(LocalDate date, long addYears, long addMonths, long addDays)
        {
            return date + Period.FromDays(addDays) + Period.FromMonths(addMonths) + Period.FromYears(addYears);
        }

        /// <summary>
        /// Korrigiert ein Datum, so dass es der Genauigkeit des AFA-Datums entspricht.
        /// </summary>
        /// <example>
        /// Wenn z.B. jeder Monat nur 30 Tage haben soll und es wird z.B. der 31.01.2008 angegeben, dann wird daraus der
        /// 30.01.2008 gemacht.
        /// </example>
        /// <param name="date">Das zu korrigierende Datum</param>
        /// <returns>Das korrigierte Datum</returns>
        public LocalDate Fix(LocalDate date)
        {
            return date;
        }

        /// <summary>
        /// Ermittelt den Tag des Jahres
        /// </summary>
        /// <param name="date">Das lokale Datum, dessen Tag des Jahres ermittelt werden soll</param>
        /// <returns>Die Anzahl an Tagen seit dem ersten Januar (01.01. == 1)</returns>
        public int GetDayOfYear(LocalDate date)
        {
            return date.DayOfYear;
        }

        /// <summary>
        /// Die Anzahl an Tagen seit dem 01.01.1753
        /// </summary>
        /// <param name="date">Das Datum für das die Anzahl der Tage seit dem 01.01.1753 ermittelt werden soll.</param>
        /// <returns>Die Anzahl an Tagen seit dem 01.01.1753</returns>
        public long GetTotalDays(LocalDate date)
        {
            return Period.Between(_startDate, date).Days;
        }
    }
}
