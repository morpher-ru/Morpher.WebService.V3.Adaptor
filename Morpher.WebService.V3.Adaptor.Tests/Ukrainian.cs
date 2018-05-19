using NUnit.Framework;
using Morpher.Ukrainian;
using Morpher.WebService.V3.Ukrainian.Adaptor;


namespace Morpher.WebService.V3.Adaptor.Test
{
    [TestFixture]
    public class Ukrainian
    {
        readonly MorpherClient _morpherClient = new MorpherClient();

        [Test]
        public void UkrainianDeclension()
        {            
            var declension = new Declension(_morpherClient.Ukrainian);

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

        [Test]
        public void UkrainianNumberSpelling()
        {
            var numberSpelling = new NumberSpelling(_morpherClient.Ukrainian);

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
