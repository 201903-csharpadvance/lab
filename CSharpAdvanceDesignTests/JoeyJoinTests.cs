using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyJoinTests
    {
        [Test]
        public void all_pets_and_owner()
        {
            var david = new Employee { FirstName = "David", LastName = "Chen" };
            var joey = new Employee { FirstName = "Joey", LastName = "Chen" };
            var tom = new Employee { FirstName = "Tom", LastName = "Chen" };

            var employees = new[]
            {
                david,
                joey,
                tom
            };

            var pets = new Pet[]
            {
                new Pet() {Name = "Lala", Owner = joey},
                new Pet() {Name = "Didi", Owner = david},
                new Pet() {Name = "Fufu", Owner = tom},
                new Pet() {Name = "QQ", Owner = joey},
            };

            var actual = JoeyJoin(
                employees,
                pets,
                e => e,
                p => p.Owner, EqualityComparer<Employee>.Default, (employee, pet) => Tuple.Create(employee.FirstName, pet.Name));

            var expected = new[]
            {
                Tuple.Create("David", "Didi"),
                Tuple.Create("Joey", "Lala"),
                Tuple.Create("Joey", "QQ"),
                Tuple.Create("Tom", "Fufu"),
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void all_pets_and_owner_get_pet_fullname()
        {
            var david = new Employee { FirstName = "David", LastName = "Li" };
            var joey = new Employee { FirstName = "Joey", LastName = "Chen" };
            var tom = new Employee { FirstName = "Tom", LastName = "Wang" };

            var employees = new[]
            {
                david,
                joey,
                tom
            };

            var pets = new Pet[]
            {
                new Pet() {Name = "Lala", Owner = joey},
                new Pet() {Name = "Didi", Owner = david},
                new Pet() {Name = "Fufu", Owner = tom},
                new Pet() {Name = "QQ", Owner = joey},
            };

            var actual = JoeyJoin(employees, pets, employee1 => employee1, pet1 => pet1.Owner, EqualityComparer<Employee>.Default, (employee, pet) => $"{pet.Name}-{employee.LastName}");

            var expected = new[]
            {
                $"Didi-Li",
                $"Lala-Chen",
                $"QQ-Chen",
                $"Fufu-Wang",
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void join_pet_partial_name_and_owner_firstName()
        {
            var employees = new[]
            {
                new Employee {FirstName = "David", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Wang"},
            };

            var pets = new Pet[]
            {
                new Pet() {Name = "Joey-Lala"},
                new Pet() {Name = "David-Didi"},
                new Pet() {Name = "Tom-Fufu"},
                new Pet() {Name = "Joey-QQ"},
            };

            var actual = JoeyJoin(
                employees,
                pets,
                e => e.FirstName,
                p => p.Name.Split('-')[0],
                EqualityComparer<string>.Default,
                (e, p) => $"{e.FirstName[0]}-{p.Name.Split('-')[1]}");

            var expected = new[]
            {
                "D-Didi",
                "J-Lala",
                "J-QQ",
                "T-Fufu",
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<TResult> JoeyJoin<TOuter, TInner, TKey, TResult>(
            IEnumerable<TOuter> outer,
            IEnumerable<TInner> inner,
            Func<TOuter, TKey> outerKeySelector,
            Func<TInner, TKey> innerKeySelector,
            IEqualityComparer<TKey> keyEqualityComparer,
            Func<TOuter, TInner, TResult> resultSelector)
        {
            var outerEnumerator = outer.GetEnumerator();

            var innerEnumerator = inner.GetEnumerator();
            while (outerEnumerator.MoveNext())
            {
                var outerCurrent = outerEnumerator.Current;

                while (innerEnumerator.MoveNext())
                {
                    var innerCurrent = innerEnumerator.Current;

                    if (keyEqualityComparer.Equals(outerKeySelector(outerCurrent), innerKeySelector(innerCurrent)))
                    {
                        yield return resultSelector(outerCurrent, innerCurrent);
                    }
                }

                innerEnumerator.Reset();
            }
        }
    }
}