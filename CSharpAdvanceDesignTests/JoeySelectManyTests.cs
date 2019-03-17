using System;
using ExpectedObjects;
using NUnit.Framework;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    [Ignore("not yet")]
    public class JoeySelectManyTests
    {
        [Test]
        public void flat_all_cities_and_sections()
        {
            var cities = new List<City>
            {
                new City {Name = "台北市", Sections = new List<string> {"大同", "大安", "松山"}},
                new City {Name = "新北市", Sections = new List<string> {"三重", "新莊"}},
            };

            var actual = JoeySelectMany(cities,
                city => city.Sections,
                (city, section) => GetZipCode(city,section));
                //(city, section) => $"{city.Name}-{section}");

            var expected = new[]
            {
                "台北市-大同",
                "台北市-大安",
                "台北市-松山",
                "新北市-三重",
                "新北市-新莊",
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private string GetZipCode(City city, string section)
        {
            var dictionary = new Dictionary<string, string>()
            {
                {"台北市大同","110" }
            };

            return dictionary[$"{city}{section}"];
        }

        private IEnumerable<string> JoeySelectMany(IEnumerable<City> cities, Func<City, IEnumerable<string>> collectionSelector, Func<City, string, string> resultSelector)
        {
            foreach (var city in cities)
            {
                foreach (var section in collectionSelector(city))
                {
                    yield return resultSelector(city, section);
                }
            }
        }
    }

    public class City
    {
        public string Name { get; set; }
        public IEnumerable<string> Sections { get; set; }
    }
}