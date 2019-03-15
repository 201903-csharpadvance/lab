using System;
using System.Collections.Generic;

namespace Lab
{
    public static class MyOwnLinq
    {
        public static List<TSource> JoeyWhere<TSource>(List<TSource> source, Predicate<TSource> predicate)
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

        public static List<TSource> JoeyWhere<TSource>(this List<TSource> source, Func<TSource, int, bool> predicate)
        {
            var result = new List<TSource>();
            var index = 0;
            foreach (var product in source)
            {
                if (predicate(product, index))
                {
                    result.Add(product);
                }

                index++;
            }

            return result;
        }
    }
}