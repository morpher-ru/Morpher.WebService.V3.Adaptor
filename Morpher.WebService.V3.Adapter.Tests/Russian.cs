using System;
using NUnit.Framework;
using Morpher.Russian;
using Morpher.WebService.V3.Russian.Adaptor;

namespace Morpher.WebService.V3.Adaptor.Test
{    

    [TestFixture]
    public class Russian
    {
        [Test]
        public void InvokeRussianWSClient()
        {            
            var declension = new Declension();

            IParse parsedResult = declension.Parse("помидор");
            Assert.IsNotNull(parsedResult);
            Assert.AreEqual("помидор", parsedResult.Nominative);
            Assert.AreEqual("помидора", parsedResult.Genitive);
            Assert.AreEqual("помидору", parsedResult.Dative);
            Assert.AreEqual("помидор", parsedResult.Accusative);
            Assert.AreEqual("помидором", parsedResult.Instrumental);
            Assert.AreEqual("помидоре", parsedResult.Prepositional);
            Assert.IsNull(parsedResult.Locative);

            Assert.AreEqual("помидоры", parsedResult.Plural.Nominative);
            Assert.AreEqual("помидоров", parsedResult.Plural.Genitive);
            Assert.AreEqual("помидорам", parsedResult.Plural.Dative);
            Assert.AreEqual("помидоры", parsedResult.Plural.Accusative);
            Assert.AreEqual("помидорами", parsedResult.Plural.Instrumental);
            Assert.AreEqual("помидорах", parsedResult.Plural.Prepositional);

            Assert.IsNull(parsedResult.Gender);
            
            Assert.Throws<NotImplementedException>(() => { var fail = parsedResult.IsAnimate; } );
            Assert.Throws<NotImplementedException>(() => { var fail = parsedResult.Paucal; });            
        }
    }
}
