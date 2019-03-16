using Lab.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public static IEnumerable<Employee> JoeyTake(this IEnumerable<Employee> employees, int takeCount)
        {
            var employeEnumerator = employees.GetEnumerator();
            var index = 0;
            while (employeEnumerator.MoveNext())
            {
                var employee = employeEnumerator.Current;

                if (index < takeCount)
                {
                    yield return employee;
                }
                else
                {
                    yield break;
                }
                index++;
            }
        }

        public static bool IsEmpty<TSource>(this IEnumerable<TSource> names)
        {
            return !names.Any();
        }

        public static bool JoeyAny(this IEnumerable<Product> products, Func<Product, bool> predicate)
        {
            var enumerator = products.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var item = enumerator.Current;
                if (predicate(item))
                {
                    return true;
                }
            }

            return false;
        }

        public static TSource JoeyFirstOrDefault<TSource>(this IEnumerable<TSource> employees)
        {
            var employeEnumerator = employees.GetEnumerator();
            return employeEnumerator.MoveNext()
                ? employeEnumerator.Current
                : default(TSource);
            //: null;
        }

        public static TSource JoeyLastOrDefault<TSource>(this IEnumerable<TSource> employees)
        {
            var enumerator = employees.GetEnumerator();
            TSource employee = default(TSource);
            while (enumerator.MoveNext())
            {
                employee = enumerator.Current;

                if (!enumerator.MoveNext())
                {
                    return employee;
                }

                //last = employee;
            }

            return employee;
        }

        public static IOrderedEnumerable<Employee> JoeyThenBy<TKey>(this IOrderedEnumerable<Employee> source,
            Func<Employee, TKey> keySelector, IComparer<TKey> comparer)
        {
            return source.CreateOrderedEnumerable(keySelector, comparer, false);
        }

        public static IOrderedEnumerable<Employee> JoeyOrderByKeepComparer(this IEnumerable<Employee> employees,
            Func<Employee, string> keySelector, Comparer<string> comparer)
        {
            return new MyOrderedEnumerable(employees, new CombineKeyComparer<string>(keySelector, comparer));
        }
    }
}