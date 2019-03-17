using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyAggregateTests
    {
        [Test]
        public void drawling_money_that_balance_have_to_be_positive()
        {
            var balance = 100.91m;

            var drawlingList = new List<int>
            {
                30, 80, 20, 40, 25
            };

            var actual = JoeyAggregate(drawlingList, balance,
                (current, seed) =>
                {
                    if (current <= seed)
                    {
                        seed -= current;
                    }

                    return seed;
                },
                seed => new Employee { Saving = seed });

            var expected = new Employee() { Saving = 10.91m };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private TResult JoeyAggregate<TResult>(IEnumerable<int> drawlingList, decimal balance,
            Func<int, decimal, decimal> func, Func<decimal, TResult> resultSelector)
        {
            var seed = balance;

            foreach (var current in drawlingList)
            {
                //seed = ((Func<decimal, int, decimal>) ((seed, current) => seed - (current <= seed ? current : 0)))(seed, current);
                seed = func(current, seed);
            }

            return resultSelector(seed);
        }
    }
}