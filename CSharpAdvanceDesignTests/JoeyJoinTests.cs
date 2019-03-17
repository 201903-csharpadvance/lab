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
                (employee, pet) => Tuple.Create(employee.FirstName, pet.Name));

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

            var actual = JoeyJoin(employees, pets,
                (employee, pet) => $"{pet.Name}-{employee.LastName}");

            var expected = new[]
            {
                $"Didi-Li",
                $"Lala-Chen",
                $"QQ-Chen",
                $"Fufu-Wang",
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<TResult> JoeyJoin<TResult>(IEnumerable<Employee> employees, IEnumerable<Pet> pets, Func<Employee, Pet, TResult> resultSelector)
        {
            var employeEnumerator = employees.GetEnumerator();

            var petEnumerator = pets.GetEnumerator();
            while (employeEnumerator.MoveNext())
            {
                var employee = employeEnumerator.Current;

                while (petEnumerator.MoveNext())
                {
                    var pet = petEnumerator.Current;

                    if (pet.Owner.Equals(employee))
                    {
                        yield return resultSelector(employee, pet);
                    }
                }

                petEnumerator.Reset();
            }
        }
    }
}