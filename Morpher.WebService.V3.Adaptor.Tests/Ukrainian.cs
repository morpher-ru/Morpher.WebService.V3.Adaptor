namespace Morpher.WebService.V3.Adaptor.Test
{
    using NUnit.Framework;
    using Morpher.Ukrainian;
    using Morpher.WebService.V3.Ukrainian.Adaptor;
    using System.Collections.Specialized;
    using Moq;

    [TestFixture]
    public class Ukrainian
    {
        const string DeclensionResultText = @"
{
    ""Р"": ""помідора"",
    ""Д"": ""помідору"",
    ""З"": ""помідора"",
    ""О"": ""помідором"",
    ""М"": ""помідорі"",
    ""К"": ""помідоре""    
}";

        [Test]
        public void UkrainianDeclension()
        {
            var webClient = new Mock<IWebClient>();
            webClient.Setup(client => client.QueryString).Returns(new NameValueCollection());
            webClient.Setup(client => client.DownloadString(It.IsAny<string>())).Returns(DeclensionResultText);
            var morpherClient = new MorpherClient(null, null, webClient.Object);

            var declension = new Declension(morpherClient.Ukrainian);

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

        const int n = 1234567890;

        const string SpellText = @"
{
    ""n"": {
        ""Н"": ""один мільярд двісті тридцять чотири мільйони п'ятсот шістдесят сім тисяч вісімсот дев'яносто"",
        ""Р"": ""одного мільярда двохсот тридцяти чотирьох мільйонів п'ятисот шістдесяти семи тисяч восьмисот дев'яноста"",
        ""Д"": ""одному мільярду двомстам тридцяти чотирьом мільйонам п'ятистам шістдесяти семи тисячам восьмистам дев'яноста"",
        ""З"": ""один мільярд двісті тридцять чотири мільйони п'ятсот шістдесят сім тисяч вісімсот дев'яносто"",
        ""О"": ""одним мільярдом двомастами тридцятьма чотирма мільйонами п'ятьмастами шістдесятьма сьома тисячами вісьмастами дев'яноста"",
        ""М"": ""одному мільярді двохстах тридцяти чотирьох мільйонах п'ятистах шістдесяти семи тисячах восьмистах дев'яноста"",
        ""К"": ""один мільярд двісті тридцять чотири мільйони п'ятсот шістдесят сім тисяч вісімсот дев'яносто""
    },
    ""unit"": {
        ""Н"": ""рублів"",
        ""Р"": ""рублів"",
        ""Д"": ""рублям"",
        ""З"": ""рублів"",
        ""О"": ""рублями"",
        ""М"": ""рублях"",
        ""К"": ""рублів""
    }
}";

        [Test]
        public void UkrainianNumberSpelling()
        {
            var webClient = new Mock<IWebClient>();
            webClient.Setup(client => client.QueryString).Returns(new NameValueCollection());
            webClient.Setup(client => client.DownloadString(It.IsAny<string>())).Returns(SpellText);
            var morpherClient = new MorpherClient(null, null, webClient.Object);

            var numberSpelling = new NumberSpelling(morpherClient.Ukrainian);

            AssertNumberSpelling(numberSpelling,
                "один мільярд двісті тридцять чотири мільйони п'ятсот шістдесят сім тисяч вісімсот дев'яносто", "рублів", Case.Nominative);

            AssertNumberSpelling(numberSpelling,
                "одного мільярда двохсот тридцяти чотирьох мільйонів п'ятисот шістдесяти семи тисяч восьмисот дев'яноста", "рублів", Case.Genitive);

            AssertNumberSpelling(numberSpelling,
                "одному мільярду двомстам тридцяти чотирьом мільйонам п'ятистам шістдесяти семи тисячам восьмистам дев'яноста", "рублям", Case.Dative);

            AssertNumberSpelling(numberSpelling,
                "один мільярд двісті тридцять чотири мільйони п'ятсот шістдесят сім тисяч вісімсот дев'яносто", "рублів", Case.Accusative);

            AssertNumberSpelling(numberSpelling,
                "одним мільярдом двомастами тридцятьма чотирма мільйонами п'ятьмастами шістдесятьма сьома тисячами вісьмастами дев'яноста", "рублями", Case.Instrumental);

            AssertNumberSpelling(numberSpelling,
                "одному мільярді двохстах тридцяти чотирьох мільйонах п'ятистах шістдесяти семи тисячах восьмистах дев'яноста", "рублях", Case.Prepositional);

            AssertNumberSpelling(numberSpelling,
                "один мільярд двісті тридцять чотири мільйони п'ятсот шістдесят сім тисяч вісімсот дев'яносто", "рублів", Case.Vocative);

            
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
