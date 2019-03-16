using Lab;
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

            var actual = employees.JoeyFirstOrDefault();

            Assert.IsNull(actual);
        }

        [Test]
        public void numbers_is_empty()
        {
            var numbers = new List<int>();
            var actual = numbers.JoeyFirstOrDefault();
            Assert.AreEqual(0, actual);
        }
    }
}