using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeySelectTests
    {
        [Test]
        public void replace_http_to_https()
        {
            var urls = GetUrls().ToList();

            var actual = JoeySelect(urls,
                url => url.Replace("http://", "https://"));

            var expected = new List<string>
            {
                "https://tw.yahoo.com",
                "https://facebook.com",
                "https://twitter.com",
                "https://github.com",
            };

            expected.ToExpectedObject().ShouldMatch(actual.ToList());
        }

        [Test]
        public void replace_http_to_https_and_append_joey()
        {
            var urls = GetUrls().ToList();

            var actual = JoeySelect(urls,
                url => url.Replace("http://", "https://") + "/joey");

            var expected = new List<string>
            {
                "https://tw.yahoo.com/joey",
                "https://facebook.com/joey",
                "https://twitter.com/joey",
                "https://github.com/joey",
            };

            expected.ToExpectedObject().ShouldMatch(actual.ToList());
        }

        private IEnumerable<string> JoeySelect(IEnumerable<string> urls, Func<string, string> mapper)
        {
            var result = new List<string>();
            foreach (var url in urls)
            {
                var mappingResult = mapper(url);
                result.Add(mappingResult);
            }

            return result;
        }

        private static IEnumerable<string> GetUrls()
        {
            yield return "http://tw.yahoo.com";
            yield return "https://facebook.com";
            yield return "https://twitter.com";
            yield return "http://github.com";
        }

        private static List<Employee> GetEmployees()
        {
            return new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "David", LastName = "Chen"}
            };
        }
    }
}