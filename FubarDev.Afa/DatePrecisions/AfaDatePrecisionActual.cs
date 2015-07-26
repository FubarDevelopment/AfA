using System;

using NodaTime;

namespace FubarDev.Afa.DatePrecisions
{
    public class AfaDatePrecisionActual : IAfaDatePrecisionHandler
    {
        private static readonly LocalDate _startDate = new LocalDate(1753, 1, 1);

        public static AfaDatePrecisionActual Default { get; } = new AfaDatePrecisionActual();

        public LocalDate Add(LocalDate date, long addYears, long addMonths, long addDays)
        {
            return date + Period.FromDays(addDays) + Period.FromMonths(addMonths) + Period.FromYears(addYears);
        }

        public LocalDate Fix(LocalDate date)
        {
            return date;
        }

        public int GetDayOfYear(LocalDate date)
        {
            return date.DayOfYear;
        }

        public long GetTotalDays(LocalDate date)
        {
            return Period.Between(_startDate, date).Days;
        }
    }
}