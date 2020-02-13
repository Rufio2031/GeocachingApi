using System.Collections.Generic;

namespace GeocachingApi.Infrastructure.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Converts an IEnumerable to a List of type T. If the collection is null just returns a new empty List of type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <returns></returns>
        public static IList<T> ToSafeList<T>(this IEnumerable<T> enumerable)
        {
            return ReferenceEquals(null, enumerable) ? new List<T>() : (enumerable is IList<T>) ? (IList<T>)enumerable : new List<T>(enumerable);
        }
    }
}
