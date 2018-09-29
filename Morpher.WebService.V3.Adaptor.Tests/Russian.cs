namespace Morpher.WebService.V3.Adaptor.Test
{
    using System;
    using NUnit.Framework;
    using Morpher.Russian;
    using Morpher.WebService.V3.Russian.Adaptor;
    using System.Collections.Specialized;
    using Moq;

    [TestFixture]
    public class Russian
    {
        string DeclensionResultText { get; } = @"
{
    ""Р"": ""помидора"",
    ""Д"": ""помидору"",
    ""В"": ""помидор"",
    ""Т"": ""помидором"",
    ""П"": ""помидоре"",    
    ""множественное"": {
        ""И"": ""помидоры"",
        ""Р"": ""помидоров"",
        ""Д"": ""помидорам"",
        ""В"": ""помидоры"",
        ""Т"": ""помидорами"",
        ""П"": ""помидорах"",        
    }
}";

        [Test]
        public void RussianDeclension()
        {
            var webClient = new Mock<IWebClient>();
            webClient.Setup(client => client.QueryString).Returns(new NameValueCollection());
            webClient.Setup(client => client.DownloadString(It.IsAny<string>())).Returns(DeclensionResultText);
            var morpherClient = new MorpherClient(null, null, webClient.Object);

            var declension = new Declension(morpherClient.Russian);

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

        const int n = 1234567890;

        string SpellResultText { get; } = @"
{
    ""n"": {
        ""И"": ""один миллиард двести тридцать четыре миллиона пятьсот шестьдесят семь тысяч восемьсот девяносто"",
        ""Р"": ""одного миллиарда двухсот тридцати четырёх миллионов пятисот шестидесяти семи тысяч восьмисот девяноста"",
        ""Д"": ""одному миллиарду двумстам тридцати четырём миллионам пятистам шестидесяти семи тысячам восьмистам девяноста"",
        ""В"": ""один миллиард двести тридцать четыре миллиона пятьсот шестьдесят семь тысяч восемьсот девяносто"",
        ""Т"": ""одним миллиардом двумястами тридцатью четырьмя миллионами пятьюстами шестьюдесятью семью тысячами восьмьюстами девяноста"",
        ""П"": ""одном миллиарде двухстах тридцати четырёх миллионах пятистах шестидесяти семи тысячах восьмистах девяноста""
    },
    ""unit"": {
        ""И"": ""рублей"",
        ""Р"": ""рублей"",
        ""Д"": ""рублям"",
        ""В"": ""рублей"",
        ""Т"": ""рублями"",
        ""П"": ""рублях""
    }
}";

        [Test]
        public void RussianNumberSpelling()
        {
            var webClient = new Mock<IWebClient>();
            webClient.Setup(client => client.QueryString).Returns(new NameValueCollection());
            webClient.Setup(client => client.DownloadString(It.IsAny<string>())).Returns(SpellResultText);
            var morpherClient = new MorpherClient(null, null, webClient.Object);

            var numberSpelling = new NumberSpelling(morpherClient.Russian);            

            AssertNumberSpelling(numberSpelling, 
                "один миллиард двести тридцать четыре миллиона пятьсот шестьдесят семь тысяч восемьсот девяносто", "рублей", Case.Nominative);

            AssertNumberSpelling(numberSpelling,
                "одного миллиарда двухсот тридцати четырёх миллионов пятисот шестидесяти семи тысяч восьмисот девяноста", "рублей", Case.Genitive);
                
            AssertNumberSpelling(numberSpelling,
                "одному миллиарду двумстам тридцати четырём миллионам пятистам шестидесяти семи тысячам восьмистам девяноста", "рублям", Case.Dative);
                
            AssertNumberSpelling(numberSpelling,
                "один миллиард двести тридцать четыре миллиона пятьсот шестьдесят семь тысяч восемьсот девяносто", "рублей", Case.Accusative);

            AssertNumberSpelling(numberSpelling,
                "одним миллиардом двумястами тридцатью четырьмя миллионами пятьюстами шестьюдесятью семью тысячами восьмьюстами девяноста", "рублями", Case.Instrumental);

            AssertNumberSpelling(numberSpelling,
                "одном миллиарде двухстах тридцати четырёх миллионах пятистах шестидесяти семи тысячах восьмистах девяноста", "рублях", Case.Prepositional);

            Assert.Throws<NotImplementedException>( () =>
            {
                string unit = "рубль";
                numberSpelling.Spell(1, ref unit, Case.Locative);
            });
            
            
            string nullUnit = null;            
            Assert.IsNull(numberSpelling.Spell(1, ref nullUnit, Case.Prepositional));
        }

        public void AssertNumberSpelling(NumberSpelling numberSpelling, string correctNumber, string correctUnit, Case @case)
        {            
            string unit = "рубль";
            string spellNumber = numberSpelling.Spell(n, ref unit, @case);

            Assert.AreEqual(correctNumber, spellNumber);
            Assert.AreEqual(correctUnit, unit);
        }
    }
}
