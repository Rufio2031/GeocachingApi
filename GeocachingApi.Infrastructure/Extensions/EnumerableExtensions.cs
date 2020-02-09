using System.Collections.Generic;

namespace GeocachingApi.Infrastructure.Extensions
{
    public static class EnumerableExtensions
    {
        public static IList<T> ToSafeList<T>(this IEnumerable<T> enumerable)
        {
            return ReferenceEquals(null, enumerable) ? new List<T>() : (enumerable is IList<T>) ? (IList<T>)enumerable : new List<T>(enumerable);
        }
    }
}
