using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    [Ignore("not yet")]
    public class JoeyPrependTests
    {
        [Test]
        public void prepend_employee_to_employees()
        {
            var employees = new Employee[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
            };

            var newEmployee = new Employee() { FirstName = "Tom", LastName = "Li" };

            var actual = JoeyPrepend(employees, newEmployee);

            var expected = new Employee[]
            {
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<Employee> JoeyPrepend(IEnumerable<Employee> employees, Employee newEmployee)
        {
            throw new System.NotImplementedException();
        }
    }
}