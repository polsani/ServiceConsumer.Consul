using System;
using System.Collections.Generic;

namespace ServiceConsumer.Consul
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            if (enumeration == null) return;

            foreach (var item in enumeration)
                action(item);
        }

        public static int ForEach<T>(this IEnumerable<T> enumeration, Action<int, T> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            var index = 0;

            foreach (var item in enumeration)
                action(index++, item);

            return index;
        }
    }
}
