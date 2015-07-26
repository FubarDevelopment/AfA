using System;

using NodaTime;

namespace FubarDev.Afa.DatePrecisions
{
    public class AfaDatePrecision30 : IAfaDatePrecisionHandler
    {
        private static readonly LocalDate _startDate = new LocalDate(1753, 1, 1);

        public static AfaDatePrecision30 Default { get; } = new AfaDatePrecision30();

        public LocalDate Add(LocalDate date, long addYears, long addMonths, long addDays)
        {
            var day = date.Day + addDays;
            var month = date.Month + addMonths;
            var year = date.Year + addYears;

            month += day / 30;
            day %= 30;

            if (day < 0)
            {
                day += 30;
                --month;
            }

            year += month / 12;
            month %= 12;
            if (month < 0)
            {
                month += 12;
                --year;
            }

            return new LocalDate((int)year, (int)month, (int)day);
        }

        public LocalDate Fix(LocalDate date)
        {
            return new LocalDate(date.Year, date.Month, date.Day > 30 ? 30 : date.Day);
        }

        public int GetDayOfYear(LocalDate date)
        {
            return (date.Month - 1) * 30 + date.Day;
        }

        public long GetTotalDays(LocalDate date)
        {
            return (date.Year - _startDate.Year) * 360 + GetDayOfYear(date);
        }
    }
}
