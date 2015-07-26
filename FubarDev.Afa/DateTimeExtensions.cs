using System;

using NodaTime;

namespace FubarDev.Afa
{
    public static class DateTimeExtensions
    {
        public static AfaDate<T> ToAfaDate<T>(this DateTime d, T handler) where T : IAfaDatePrecisionHandler
        {
            return new AfaDate<T>(LocalDateTime.FromDateTime(d).Date, handler);
        }

        public static AfaDate<T>? ToAfaDate<T>(this DateTime? d, T handler) where T : IAfaDatePrecisionHandler
        {
            if (d == null)
                return null;
            return new AfaDate<T>(LocalDateTime.FromDateTime(d.Value).Date, handler);
        }

        public static AfaDate<T> Round<T>(this AfaDate<T> d, AfaDateRounding mode) where T : IAfaDatePrecisionHandler
        {
            return d.Round(mode);
        }

        public static AfaDate<T>? Round<T>(this AfaDate<T>? d, AfaDateRounding mode) where T : IAfaDatePrecisionHandler
        {
            return d?.Round(mode);
        }
    }
}
