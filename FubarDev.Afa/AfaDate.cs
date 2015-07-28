using System;

using NodaTime;

namespace FubarDev.Afa
{
    public struct AfaDate<T> : IComparable<AfaDate<T>>, IComparable, IEquatable<AfaDate<T>>
        where T : IAfaDatePrecisionHandler
    {
        private readonly IAfaDatePrecisionHandler _handler;

        public int Year { get; }
        public int Month { get; }
        public int Day { get; }
        public int DayOfYear => _handler.GetDayOfYear(new LocalDate(Year, Month, Day));

        public long TotalDays => _handler.GetTotalDays(new LocalDate(Year, Month, Day));

        public AfaDate<T> BeginOfMonth => new AfaDate<T>(new LocalDate(Year, Month, 1), _handler);

        public AfaDate<T> EndOfMonth => new AfaDate<T>(_handler.Add(new LocalDate(Year, Month, 1), 0, 1, -1), _handler);

        public AfaDate(LocalDate dt, IAfaDatePrecisionHandler handler)
            : this(dt.Year, dt.Month, dt.Day, handler)
        {
        }

        public AfaDate(int year, int month, int day, IAfaDatePrecisionHandler handler)
            : this()
        {
            _handler = handler;

            var dt = handler.Fix(new LocalDate(year, month, day));

            Year = dt.Year;
            Month = dt.Month;
            Day = dt.Day;
        }

        public int CompareTo(AfaDate<T> other)
        {
            return CompareTo(other.TotalDays);
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            return CompareTo((AfaDate<T>)obj);
        }

        public bool Equals(AfaDate<T> other)
        {
            return CompareTo(other) == 0;
        }

        public override bool Equals(object obj)
        {
            return Equals((AfaDate<T>)obj);
        }

        public override int GetHashCode()
        {
            return TotalDays.GetHashCode();
        }

        public AfaDate<T> AddDays(long days)
        {
            return new AfaDate<T>(_handler.Add(new LocalDate(Year, Month, Day), 0, 0, days), _handler);
        }

        public AfaDate<T> AddMonths(long months)
        {
            return new AfaDate<T>(_handler.Add(new LocalDate(Year, Month, Day), 0, months, 0), _handler);
        }

        public AfaDate<T> AddYears(long years)
        {
            return new AfaDate<T>(_handler.Add(new LocalDate(Year, Month, Day), years, 0, 0), _handler);
        }

        public AfaDate<T> Add(Period period)
        {
            return AddDays(period.Days);
        }

        public static Period operator -(AfaDate<T> d1, AfaDate<T> d2)
        {
            return Period.FromDays(d1.TotalDays - d2.TotalDays);
        }

        public static AfaDate<T> operator -(AfaDate<T> d, Period t)
        {
            return d.AddDays(-(int)t.Days);
        }

        public static AfaDate<T> operator +(AfaDate<T> d, Period t)
        {
            return d.Add(t);
        }

        public static bool operator ==(AfaDate<T> d1, AfaDate<T> d2)
        {
            return d1.CompareTo(d2) == 0;
        }

        public static bool operator >(AfaDate<T> d1, AfaDate<T> d2)
        {
            return d1.CompareTo(d2) > 0;
        }

        public static bool operator >=(AfaDate<T> d1, AfaDate<T> d2)
        {
            return d1.CompareTo(d2) >= 0;
        }

        public static bool operator !=(AfaDate<T> d1, AfaDate<T> d2)
        {
            return d1.CompareTo(d2) != 0;
        }

        public static bool operator <(AfaDate<T> d1, AfaDate<T> d2)
        {
            return d1.CompareTo(d2) < 0;
        }

        public static bool operator <=(AfaDate<T> d1, AfaDate<T> d2)
        {
            return d1.CompareTo(d2) <= 0;
        }

        public static implicit operator LocalDate(AfaDate<T> d)
        {
            return new LocalDate(d.Year, d.Month, d.Day);
        }

        public static implicit operator LocalDate? (AfaDate<T>? d)
        {
            if (!d.HasValue)
                return null;
            return new LocalDate(d.Value.Year, d.Value.Month, d.Value.Day);
        }

        public AfaDate<T> Round(AfaDateRounding mode)
        {
            LocalDate result;
            switch (mode)
            {
                case AfaDateRounding.Day:
                    result = this;
                    break;
                case AfaDateRounding.Month:
                    result = new LocalDate(Year, Month, 1);
                    if (Day >= 15)
                        result = result + Period.FromMonths(1);
                    break;
                case AfaDateRounding.BeginOfMonth:
                    result = new LocalDate(Year, Month, 1);
                    break;
                case AfaDateRounding.HalfYear:
                    if (Month >= 7)
                    {
                        result = new LocalDate(Year, 7, 1);
                    }
                    else
                    {
                        result = new LocalDate(Year, 1, 1);
                    }
                    break;
                case AfaDateRounding.BeginOfYear:
                    result = new LocalDate(Year, 1, 1);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return new AfaDate<T>(result, _handler);
        }
    }
}
