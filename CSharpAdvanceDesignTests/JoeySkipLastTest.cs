using ExpectedObjects;
using NUnit.Framework;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeySkipLastTest
    {
        [Test]
        public void skip_last_2()
        {
            var numbers = new[] { 10, 20, 30, 40, 50 };
            var actual = JoeySkipLast(numbers, 2);

            var expected = new[] { 10, 20, 30 };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<int> JoeySkipLast(IEnumerable<int> numbers, int count)
        {
            var queue = new Queue<int>();
            var enumerator = numbers.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (queue.Count == count)
                {
                    yield return queue.Dequeue();
                }
                queue.Enqueue(current);
            }

            //var queue = new Queue<int>(numbers);
            //var queueCount = queue.Count;
            //for (int i = 0; i < queueCount - count; i++)
            //{
            //    yield return queue.Dequeue();
            //}
        }
    }
}