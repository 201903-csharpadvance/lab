using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyGroupByTests
    {
        [Test]
        public void groupBy_lastName()
        {
            var employees = new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Lee"},
                new Employee {FirstName = "Eric", LastName = "Chen"},
                new Employee {FirstName = "John", LastName = "Chen"},
                new Employee {FirstName = "David", LastName = "Lee"},
            };

            var actual = JoeyGroupBy(employees,
                employee => employee.LastName);

            Assert.AreEqual(2, actual.Count());
            var firstGroup = new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Eric", LastName = "Chen"},
                new Employee {FirstName = "John", LastName = "Chen"},
            };

            firstGroup.ToExpectedObject().ShouldMatch(actual.First().ToList());
        }

        private IEnumerable<IGrouping<string, Employee>> JoeyGroupBy(IEnumerable<Employee> employees,
            Func<Employee, string> groupKeySelector)
        {
            var lookup = new MyLookup();
            foreach (var employee in employees)
            {
                lookup.AddElement(groupKeySelector(employee), employee);
            }

            return lookup;
        }
    }

    internal class MyLookup : IEnumerable<IGrouping<string, Employee>>
    {
        private Dictionary<string, List<Employee>> _myLookup = new Dictionary<string, List<Employee>>();

        public IEnumerator<IGrouping<string, Employee>> GetEnumerator()
        {
            return _myLookup.Select(x => new MyGrouping(x.Key, x.Value)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void AddElement(string key, Employee value)
        {
            if (_myLookup.ContainsKey(key))
            {
                _myLookup[key].Add(value);
            }
            else
            {
                _myLookup.Add(key, new List<Employee> { value });
            }
        }
    }

    public class MyGrouping : IGrouping<string, Employee>
    {
        private readonly IEnumerable<Employee> _collection;

        public MyGrouping(string key, IEnumerable<Employee> collection)
        {
            Key = key;
            _collection = collection;
        }

        public IEnumerator<Employee> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public string Key { get; }
    }
}