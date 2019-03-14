using NUnit.Framework;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    [Ignore("not yet")]
    public class JoeyCountTests
    {
        [Test]
        public void count_of_numbers()
        {
            var numbers = new[] { 10, 20, 30, 40, 50 };

            var count = JoeyCount(numbers);
            var expected = 5;
            Assert.AreEqual(expected, count);
        }

        private int JoeyCount(IEnumerable<int> numbers)
        {
            throw new System.NotImplementedException();
        }
    }
}