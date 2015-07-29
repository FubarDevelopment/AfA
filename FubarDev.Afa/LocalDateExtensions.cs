using System;

using NodaTime;

namespace FubarDev.Afa
{
    /// <summary>
    /// Erweiterungen für die Umwandlung von DateTime in <see cref="AfaDate{T}"/>
    /// </summary>
    public static class LocalDateExtensions
    {
        /// <summary>
        /// Ermittelt den letzten Tag des Monats
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static LocalDate GetLastDayOfMonth(this LocalDate date)
        {
            return new LocalDate(date.Year, date.Month, 1) + Period.FromMonths(1) - Period.FromDays(1);
        }

        /// <summary>
        /// Rundung des AfA-Datums
        /// </summary>
        /// <param name="date">Das zu rundende Datum</param>
        /// <param name="mode">Der Rundungsmodus</param>
        /// <returns>Das gerundete AfA-Datum</returns>
        public static LocalDate Round(this LocalDate date, DateRoundingMode mode)
        {
            LocalDate result;
            switch (mode)
            {
                case DateRoundingMode.Day:
                    result = date;
                    break;
                case DateRoundingMode.Month:
                    result = new LocalDate(date.Year, date.Month, 1);
                    if (date.Day >= 15)
                        result = result + Period.FromMonths(1);
                    break;
                case DateRoundingMode.BeginOfMonth:
                    result = new LocalDate(date.Year, date.Month, 1);
                    break;
                case DateRoundingMode.HalfYear:
                    if (date.Month >= 7)
                    {
                        result = new LocalDate(date.Year, 7, 1);
                    }
                    else
                    {
                        result = new LocalDate(date.Year, 1, 1);
                    }
                    break;
                case DateRoundingMode.BeginOfYear:
                    result = new LocalDate(date.Year, 1, 1);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return result;
        }
    }
}
