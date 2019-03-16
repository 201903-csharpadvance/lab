using NUnit.Framework;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeySequenceEqualTests
    {
        [Test]
        public void compare_two_numbers_equal()
        {
            var first = new List<int> {3, 2, 1};
            var second = new List<int> {3, 2, 1};

            var actual = JoeySequenceEqual(first, second);

            Assert.IsTrue(actual);
        }

        private bool JoeySequenceEqual(IEnumerable<int> first, IEnumerable<int> second)
        {
            var firstEnumerator = first.GetEnumerator();
            var secondEnumerator = second.GetEnumerator();

            while (true)
            {
                var firstFlag = firstEnumerator.MoveNext();
                var secondFlag = secondEnumerator.MoveNext();

                if (IsLengthDifferent(firstFlag, secondFlag))
                {
                    return false;
                }

                if (IsValueDifferent(firstEnumerator, secondEnumerator))
                {
                    return false;
                }

                if (IsEnd(firstFlag))
                {
                    return true;
                }
            }
        }

        private static bool IsEnd(bool firstFlag)
        {
            return !firstFlag;
        }

        private static bool IsValueDifferent(IEnumerator<int> firstEnumerator, IEnumerator<int> secondEnumerator)
        {
            return firstEnumerator.Current != secondEnumerator.Current;
        }

        private static bool IsLengthDifferent(bool firstFlag, bool secondFlag)
        {
            return firstFlag != secondFlag;
        }
    }
}