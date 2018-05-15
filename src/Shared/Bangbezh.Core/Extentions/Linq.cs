using System;
using System.Collections.Generic;
using System.Text;

namespace System.Linq
{
    public static class Linq
    {
        public static TItem HasMinOrDefault<TItem, TValue>(this IEnumerable<TItem> list, Func<TItem, TValue> value) where TValue : IComparable<TValue>
        {
            if (!list.Any())
                return default(TItem);

            TItem min = default(TItem);

            foreach (var item in list)
            {
                if (min?.Equals(default(TItem)) != false || value(min).CompareTo(value(item)) > 0)
                    min = item;
            }

            return min;
        }
    }
}
