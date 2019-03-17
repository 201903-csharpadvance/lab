using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyCastTests
    {
        [Test]
        public void cast_int_exception_when_ArrayList_has_string()
        {
            var arrayList = new ArrayList { 1, "a", 3 };

            TestDelegate action = () => JoeyCast<int>(arrayList).ToArray();
            //void TestDelegate() => JoeyCast<int>(arrayList);

            Assert.Throws<JoeyCastException>(action);
        }

        private IEnumerable<T> JoeyCast<T>(IEnumerable source)
        {
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (current is T item)
                {
                    yield return item;
                }
                else
                {
                    throw new JoeyCastException();
                }
            }
        }
    }

    public class JoeyCastException : Exception
    {
    }
}