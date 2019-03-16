using Lab.Entities;
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
            var first = new List<int> { 3, 2, 1 };
            var second = new List<int> { 3, 2, 1 };

            var actual = JoeySequenceEqual(first, second);

            Assert.IsTrue(actual);
        }

        [Test]
        public void equality_compare_with_two_employees()
        {
            var first = new List<Employee>
            {
                new Employee() {FirstName = "Joey", LastName = "Chen"},
                new Employee() {FirstName = "Tom", LastName = "Li"},
                new Employee() {FirstName = "David", LastName = "Wang"},
            };

            var second = new List<Employee>
            {
                new Employee() {FirstName = "Joey", LastName = "Chen"},
                new Employee() {FirstName = "Tom", LastName = "Li"},
                new Employee() {FirstName = "David", LastName = "Wang"},
            };

            var actual = JoeySequenceEqual(first, second, new JoeyEqualityComparer());
            Assert.IsTrue(actual);
        }

        private bool JoeySequenceEqual<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            return JoeySequenceEqual(first, second, EqualityComparer<TSource>.Default);
        }

        private bool JoeySequenceEqual<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> equalityComparer)
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

                if (IsEnd(firstFlag))
                {
                    return true;
                }

                if (!equalityComparer.Equals(firstEnumerator.Current, secondEnumerator.Current))
                {
                    return false;
                }
            }
        }

        private static bool IsEnd(bool firstFlag)
        {
            return !firstFlag;
        }

        private static bool IsLengthDifferent(bool firstFlag, bool secondFlag)
        {
            return firstFlag != secondFlag;
        }
    }

    internal class JoeyEqualityComparer : IEqualityComparer<Employee>
    {
        public bool Equals(Employee x, Employee y)
        {
            return x.LastName == y.LastName && x.FirstName == y.FirstName;
        }

        public int GetHashCode(Employee obj)
        {
            throw new System.NotImplementedException();
        }
    }
}