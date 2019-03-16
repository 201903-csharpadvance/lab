using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
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

    public class CombineKeyComparer<TKey> : IComparer<Employee>
    {
        public CombineKeyComparer(Func<Employee, TKey> keySelector, IComparer<TKey> keyComparer)
        {
            KeySelector = keySelector;
            KeyComparer = keyComparer;
        }

        private Func<Employee, TKey> KeySelector { get; set; }
        private IComparer<TKey> KeyComparer { get; set; }

        public int Compare(Employee element, Employee minElement)
        {
            return KeyComparer.Compare(KeySelector(element), KeySelector(minElement));
        }
    }

    public class ComboComparer : IComparer<Employee>
    {
        public ComboComparer(IComparer<Employee> firstComparer, IComparer<Employee> secondComparer)
        {
            FirstComparer = firstComparer;
            SecondComparer = secondComparer;
        }

        public IComparer<Employee> FirstComparer { get; private set; }
        public IComparer<Employee> SecondComparer { get; private set; }

        public int Compare(Employee x, Employee y)
        {
            var firstCompareResult = FirstComparer.Compare(x, y);
            if (firstCompareResult == 0)
            {
                return SecondComparer.Compare(x, y);
            }

            return firstCompareResult;
        }
    }

    [TestFixture]
    public class JoeyOrderByTests
    {
        [Test]
        public void orderBy_lastName()
        {
            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Wang"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
            };

            var actual = JoeyOrderByKeepComparer(employees, element => element.LastName, Comparer<string>.Default);

            var expected = new[]
            {
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Wang"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void orderBy_lastName_and_firstName()
        {
            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Wang"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
            };

            var firstComparer = new CombineKeyComparer<string>(element => element.LastName, Comparer<string>.Default);
            var secondComparer = new CombineKeyComparer<string>(element => element.FirstName, Comparer<string>.Default);

            var firstCombo = new ComboComparer(firstComparer, secondComparer);

            var actual = JoeyOrderBy(employees,
                firstCombo);

            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Wang"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void lastName_firstName_Age()
        {
            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Wang", Age = 50},
                new Employee {FirstName = "Tom", LastName = "Li", Age = 31},
                new Employee {FirstName = "Joseph", LastName = "Chen", Age = 32},
                new Employee {FirstName = "Joey", LastName = "Chen", Age = 33},
                new Employee {FirstName = "Joey", LastName = "Wang", Age = 20},
            };

            var orderByLastName = JoeyOrderByKeepComparer(employees, e => e.LastName, Comparer<string>.Default);

            var orderByFirstName = JoeyThenBy(orderByLastName, e => e.FirstName, Comparer<string>.Default);

            var actual = JoeyThenBy(orderByFirstName, e => e.Age, Comparer<int>.Default);
            //var firstComparer = new CombineKeyComparer<string>(element => element.LastName, Comparer<string>.Default);
            //var secondComparer = new CombineKeyComparer<string>(element => element.FirstName, Comparer<string>.Default);

            //var firstCombo = new ComboComparer(firstComparer, secondComparer);

            //var thirdComparer = new CombineKeyComparer<int>(element => element.Age, Comparer<int>.Default);

            //var finalCombo = new ComboComparer(firstCombo, thirdComparer);

            //var actual = JoeyOrderBy(employees, finalCombo);

            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen", Age = 33},
                new Employee {FirstName = "Joseph", LastName = "Chen", Age = 32},
                new Employee {FirstName = "Tom", LastName = "Li", Age = 31},
                new Employee {FirstName = "Joey", LastName = "Wang", Age = 20},
                new Employee {FirstName = "Joey", LastName = "Wang", Age = 50},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IOrderedEnumerable<Employee> JoeyThenBy<TKey>(IOrderedEnumerable<Employee> source,
            Func<Employee, TKey> keySelector, IComparer<TKey> comparer)
        {
            return source.CreateOrderedEnumerable(keySelector, comparer, false);
        }

        private IOrderedEnumerable<Employee> JoeyOrderByKeepComparer(IEnumerable<Employee> employees,
            Func<Employee, string> keySelector, Comparer<string> comparer)
        {
            var myOrderedEnumerable =
                new MyOrderedEnumerable(employees, new CombineKeyComparer<string>(keySelector, comparer));
            return myOrderedEnumerable;
        }

        private IEnumerable<Employee> JoeyOrderBy(
            IEnumerable<Employee> employees,
            IComparer<Employee> comparer)
        {
            //bubble sort
            var elements = employees.ToList();
            while (elements.Any())
            {
                var minElement = elements[0];
                var index = 0;
                for (int i = 1; i < elements.Count; i++)
                {
                    var element = elements[i];

                    if (comparer.Compare(element, minElement) < 0)
                    {
                        minElement = element;
                        index = i;
                    }
                }

                elements.RemoveAt(index);
                yield return minElement;
            }
        }
    }
}