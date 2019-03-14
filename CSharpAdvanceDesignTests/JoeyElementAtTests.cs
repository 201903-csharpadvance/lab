using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    [Ignore("not yet")]
    public class JoeyElementAtTests
    {
        [Test]
        public void get_the_2nd_element_of_enumerable()
        {
            var employees = new List<Employee>
            {
                new Employee{FirstName = "Joey",LastName = "Chen"},
                new Employee{FirstName = "Tom",LastName = "Li"},
                new Employee{FirstName = "David",LastName = "Wang"},
            };

            var actual = JoeyElementAt(employees, 1);

            var expected = new Employee { FirstName = "Tom", LastName = "Li" };

            expected.ToExpectedObject().ShouldEqual(actual);
        }

        private Employee JoeyElementAt(IEnumerable<Employee> employees, int index)
        {
            throw new System.NotImplementedException();
        }
    }
}