using System;
using NUnit.Framework;
using Morpher.Ukrainian;
using Morpher.WebService.V3.Ukrainian.Adaptor;


namespace Morpher.WebService.V3.Adaptor.Test
{
    [TestFixture]
    public class Ukrainian
    {
        [Test]
        public void InvokeUkrainianWSClient()
        {            
            var declension = new Declension();

            IParse parsedResult = declension.Parse("помідор");
            Assert.IsNotNull(parsedResult);
            Assert.AreEqual("помідор", parsedResult.Nominative);
            Assert.AreEqual("помідора", parsedResult.Genitive);
            Assert.AreEqual("помідору", parsedResult.Dative);
            Assert.AreEqual("помідора", parsedResult.Accusative);
            Assert.AreEqual("помідором", parsedResult.Instrumental);
            Assert.AreEqual("помідорі", parsedResult.Prepositional);
            Assert.AreEqual("помідоре", parsedResult.Vocative);
            Assert.IsNull(parsedResult.Gender);
        }
    }
}
