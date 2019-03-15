using System;
using System.Collections.Generic;

namespace Lab
{
    public static class MyOwnLinq
    {
        public static IEnumerable<TSource> JoeyWhere<TSource>(this IEnumerable<TSource> source,
            Predicate<TSource> predicate)
        {
            var sourcEnumerator = source.GetEnumerator();
            while (sourcEnumerator.MoveNext())
            {
                var item = sourcEnumerator.Current;
                if (predicate(item))
                {
                    yield return item;
                }
            }

            //foreach (var product in source)
            //{
            //    if (predicate(product))
            //    {
            //        //yield return product;
            //    }
            //}
        }

        public static IEnumerable<TSource> JoeyWhere<TSource>(this IEnumerable<TSource> source,
            Func<TSource, int, bool> predicate)
        {
            var sourceEnumerator = source.GetEnumerator();
            var index = 0;
            while (sourceEnumerator.MoveNext())
            {
                var item = sourceEnumerator.Current;
                if (predicate(item, index))
                {
                    yield return item;
                }

                index++;
            }

            //var result = new List<TSource>();
            //var index = 0;
            //foreach (var product in source)
            //{
            //    if (predicate(product, index))
            //    {
            //        result.Add(product);
            //    }

            //    index++;
            //}

            //return result;
        }

        public static IEnumerable<TResult> JoeySelect<TSource, TResult>(this IEnumerable<TSource> source,
            Func<TSource, int, TResult> selector)
        {
            var result = new List<TResult>();
            var index = 0;
            foreach (var url in source)
            {
                result.Add(selector(url, index));
                index++;
            }

            return result;
        }

        public static IEnumerable<TResult> JoeySelect<TSource, TResult>(this IEnumerable<TSource> source,
            Func<TSource, TResult> selector)
        {
            //var result = new List<TResult>();
            foreach (var url in source)
            {
                yield return selector(url);
                //result.Add(selector(url));
            }

            //return result;
        }
    }
}