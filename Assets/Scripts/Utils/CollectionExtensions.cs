using System;
using System.Collections.Generic;

namespace LPTask.Utils
{
    public static class CollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> self, IEnumerable<T> other)
        {
            foreach (var item in other)
            {
                self.Add(item);
            }
        }

        public static void Foreach<T>(this IEnumerable<T> self, Action<T> action)
        {
            foreach (var item in self)
            {
                action.Invoke(item);
            }
        }
    }
}