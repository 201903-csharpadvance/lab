using System;
using System.Collections.Generic;

namespace Lab
{
    public static class MyOwnLinq
    {
        public static List<TSource> JoeyWhere<TSource>(this List<TSource> source, Func<TSource, bool> predicate)
        {
            var result = new List<TSource>();
            foreach (var product in source)
            {
                if (predicate(product))
                {
                    result.Add(product);
                }
            }

            return result;
        }
    }
}