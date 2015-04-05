using System;
using System.Collections.Generic;

namespace MyFridge.Extensions
{
    public static class CollectionExt
    {

        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException("items");

            foreach (var item in items)
            {
                collection.Add(item);
            }
        }
    }
}