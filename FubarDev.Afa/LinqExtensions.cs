using System;
using System.Collections.Generic;

namespace FubarDev.Afa
{
    static class LinqExtensions
    {
        /// <summary>
        /// Ruft eine Projektion mit jeweils dem vorherigen und aktuellen Wert auf.
        /// </summary>
        /// <typeparam name="TSource">Der Typ der Sequenz die Verarbeitet wird</typeparam>
        /// <typeparam name="TResult">Der Ergebnis-Typ der Projektion</typeparam>
        /// <param name="source">Die Quell-Enumeration</param>
        /// <param name="projection">Die Projektion, die aus dem vorherigen und aktuellen Wert einer <paramref name="source"/> das Ergebnis liefert.</param>
        /// <returns>Das Ergebnis der <paramref name="projection"/></returns>
        public static IEnumerable<TResult> SelectWithPrevious<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TSource, TResult> projection)
        {
            using (var iterator = source.GetEnumerator())
            {
                if (!iterator.MoveNext())
                    yield break;
                TSource previous = iterator.Current;
                while (iterator.MoveNext())
                {
                    yield return projection(previous, iterator.Current);
                    previous = iterator.Current;
                }
            }
        }
    }
}
