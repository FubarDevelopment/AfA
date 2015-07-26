using NodaTime;

namespace FubarDev.Afa
{
    public interface IAfaDatePrecisionHandler
    {
        int GetDayOfYear(LocalDate date);

        long GetTotalDays(LocalDate date);

        LocalDate Add(LocalDate date, long addYears, long addMonths, long addDays);

        LocalDate Fix(LocalDate date);
    }
}
