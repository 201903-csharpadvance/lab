using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    public class CombineKeyComparer : IComparer<Employee>
    {
        public CombineKeyComparer(Func<Employee, string> keySelector, IComparer<string> keyComparer)
        {
            KeySelector = keySelector;
            KeyComparer = keyComparer;
        }

        private Func<Employee, string> KeySelector { get; set; }
        private IComparer<string> KeyComparer { get; set; }

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
        //[Test]
        //[Ignore("temp")]
        //public void orderBy_lastName()
        //{
        //    var employees = new[]
        //    {
        //        new Employee {FirstName = "Joey", LastName = "Wang"},
        //        new Employee {FirstName = "Tom", LastName = "Li"},
        //        new Employee {FirstName = "Joseph", LastName = "Chen"},
        //        new Employee {FirstName = "Joey", LastName = "Chen"},
        //    };

        //    var actual = JoeyOrderByLastName(employees, element => element.LastName);

        //    var expected = new[]
        //    {
        //        new Employee {FirstName = "Joseph", LastName = "Chen"},
        //        new Employee {FirstName = "Joey", LastName = "Chen"},
        //        new Employee {FirstName = "Tom", LastName = "Li"},
        //        new Employee {FirstName = "Joey", LastName = "Wang"},
        //    };

        //    expected.ToExpectedObject().ShouldMatch(actual);
        //}

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

            var firstComparer = new CombineKeyComparer(element => element.LastName, Comparer<string>.Default);
            var secondComparer = new CombineKeyComparer(element => element.FirstName, Comparer<string>.Default);

            var actual = JoeyOrderByLastNameAndFirstName(employees,
                new ComboComparer(firstComparer, secondComparer));

            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Wang"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<Employee> JoeyOrderByLastNameAndFirstName(
            IEnumerable<Employee> employees,
            ComboComparer comboComparer)
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

                    if (comboComparer.Compare(element, minElement) < 0)
                    {
                        minElement = element;
                        index = i;
                    }

                    //var firstCompareResult = comboComparer.FirstComparer.Compare(element, minElement);
                    //if (firstCompareResult < 0)
                    //{
                    //    minElement = element;
                    //    index = i;
                    //}
                    //else if (firstCompareResult == 0)
                    //{
                    //    var secondCompareResult = comboComparer.SecondComparer.Compare(element, minElement);
                    //    if (secondCompareResult < 0)
                    //    {
                    //        minElement = element;
                    //        index = i;
                    //    }
                    //}
                }

                elements.RemoveAt(index);
                yield return minElement;
            }
        }
    }
}