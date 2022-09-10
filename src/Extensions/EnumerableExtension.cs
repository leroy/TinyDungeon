using System;
using System.Collections.Generic;

namespace TinyDungeon.Extensions
{
    public static class EnumerableExtension
    {
        public static IEnumerable<T> Transform<T>(
            this IEnumerable<T> source,
            Action<T> act)
        {
            foreach (T element in source) act(element);
            return source;
        }
    }
}