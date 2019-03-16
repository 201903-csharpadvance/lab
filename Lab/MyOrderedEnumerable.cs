using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Lab.Entities;

namespace Lab
{
    public class MyOrderedEnumerable : IOrderedEnumerable<Employee>
    {
        private readonly IEnumerable<Employee> _source;
        private readonly IComparer<Employee> _untilNowComboComparer;

        public MyOrderedEnumerable(IEnumerable<Employee> source, IComparer<Employee> untilNowComboComparer)
        {
            _source = source;
            _untilNowComboComparer = untilNowComboComparer;
        }

        public IOrderedEnumerable<Employee> CreateOrderedEnumerable<TKey>(Func<Employee, TKey> keySelector,
            IComparer<TKey> lastComparer, bool @descending)
        {
            var comparer = new CombineKeyComparer<TKey>(keySelector, lastComparer);
            return new MyOrderedEnumerable(_source, new ComboComparer(_untilNowComboComparer, comparer));
        }

        public IEnumerator<Employee> GetEnumerator()
        {
            //bubble sort
            var elements = _source.ToList();
            while (elements.Any())
            {
                var minElement = elements[0];
                var index = 0;
                for (int i = 1; i < elements.Count; i++)
                {
                    var element = elements[i];

                    if (_untilNowComboComparer.Compare(element, minElement) < 0)
                    {
                        minElement = element;
                        index = i;
                    }
                }

                elements.RemoveAt(index);
                yield return minElement;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}