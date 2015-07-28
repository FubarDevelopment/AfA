using System;

using NodaTime;

namespace FubarDev.Afa
{
    /// <summary>
    /// Erweiterungen für die Umwandlung von DateTime in <see cref="AfaDate{T}"/>
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Erstellt ein <see cref="AfaDate{T}"/> aus einem <see cref="DateTime"/> mit der angegegeben Genauigkeit.
        /// </summary>
        /// <typeparam name="T">Die Klasse die die Genauigkeit des <see cref="AfaDate{T}"/> spezifiziert</typeparam>
        /// <param name="d">Das umzuwandelnde <see cref="DateTime"/></param>
        /// <param name="handler">Die Implementation der <see cref="AfaDate{T}"/>-Genauigkeit</param>
        /// <returns>Das umgewandelte <see cref="AfaDate{T}"/></returns>
        public static AfaDate<T> ToAfaDate<T>(this DateTime d, T handler) where T : IAfaDatePrecisionHandler
        {
            return new AfaDate<T>(LocalDateTime.FromDateTime(d).Date, handler);
        }

        /// <summary>
        /// Erstellt ein <see cref="AfaDate{T}"/> aus einem <see cref="DateTime"/> mit der angegegeben Genauigkeit.
        /// </summary>
        /// <typeparam name="T">Die Klasse die die Genauigkeit des <see cref="AfaDate{T}"/> spezifiziert</typeparam>
        /// <remarks>
        /// Wenn <paramref name="d"/> <code>null</code> ist, dann wird auch <code>null</code> zurückgeliefert.
        /// </remarks>
        /// <param name="d">Das umzuwandelnde <see cref="DateTime"/></param>
        /// <param name="handler">Die Implementation der <see cref="AfaDate{T}"/>-Genauigkeit</param>
        /// <returns>Das umgewandelte <see cref="AfaDate{T}"/> oder <code>null</code></returns>
        public static AfaDate<T>? ToAfaDate<T>(this DateTime? d, T handler) where T : IAfaDatePrecisionHandler
        {
            if (d == null)
                return null;
            return new AfaDate<T>(LocalDateTime.FromDateTime(d.Value).Date, handler);
        }
    }
}
