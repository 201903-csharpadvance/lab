using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyFirstOrDefaultTests
    {
        [Test]
        public void get_null_when_employees_is_empty()
        {
            var employees = new List<Employee>();

            var actual = JoeyFirstOrDefault(employees);

            Assert.IsNull(actual);
        }

        [Test]
        public void numbers_is_empty()
        {
            var numbers = new List<int>();
            var actual = JoeyFirstOrDefault(numbers);
            Assert.AreEqual(0, actual);
        }

        private TSource JoeyFirstOrDefault<TSource>(IEnumerable<TSource> employees)
        {
            var employeEnumerator = employees.GetEnumerator();
            return employeEnumerator.MoveNext()
                ? employeEnumerator.Current
                : default(TSource);
            //: null;
        }
    }
}