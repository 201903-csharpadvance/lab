using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyConcatTests
    {
        [Test]
        public void concat_two_employees()
        {
            var first = new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
            };

            var second = new List<Employee>
            {
                new Employee {FirstName = "David", LastName = "Li"},
                new Employee {FirstName = "Tom", LastName = "Wang"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
            };

            var actual = JoeyConcat(first, second);

            var expected = new List<Employee>()
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "David", LastName = "Li"},
                new Employee {FirstName = "Tom", LastName = "Wang"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<Employee> JoeyConcat(IEnumerable<Employee> first, IEnumerable<Employee> second)
        {
            var firstEnumerator = first.GetEnumerator();
            while (firstEnumerator.MoveNext())
            {
                yield return firstEnumerator.Current;
            }

            var secondEnumerator = second.GetEnumerator();
            while (secondEnumerator.MoveNext())
            {
                yield return secondEnumerator.Current;
            }
        }
    }
}