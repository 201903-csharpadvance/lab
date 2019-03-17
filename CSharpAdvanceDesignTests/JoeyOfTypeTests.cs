using Lab;
using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyOfTypeTests
    {
        [Test]
        public void get_special_type_value_from_arguments()
        {
            //ActionExecutingContext.ActionArguments: Dictionary<string,object>

            var arguments = new Dictionary<string, object>
            {
                {"validator1", new ProfitValidator()},
                {"validator2", new ProductPriceValidator()},
                {"model", new Product {Price = 100, Cost = 111}},
            };

            var validators = JoeyOfType<IValidator<Product>>(arguments.Values);

            var product = JoeyOfType<Product>(arguments.Values).Single();

            var isValid = validators.All(x => x.Validate(product));

            Assert.IsFalse(isValid);
            //Assert.AreEqual(2, validators.Count());
        }

        private IEnumerable<TResult> JoeyOfType<TResult>(IEnumerable values)
        {
            var enumerator = values.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (current is TResult c)
                {
                    yield return c;
                }
            }
        }
    }
}