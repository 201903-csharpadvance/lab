using NUnit.Framework;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    [Ignore("not yet")]
    public class JoeyMaxTests
    {
        [Test]
        public void get_max_number()
        {
            var numbers = new[] { 1, 3, 91, 5 };

            var max = JoeyMax(numbers);

            Assert.AreEqual(91, max);
        }

        private int JoeyMax(IEnumerable<int> numbers)
        {
            throw new System.NotImplementedException();
        }
    }
}