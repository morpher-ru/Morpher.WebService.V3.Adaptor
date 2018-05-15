using Morpher.Russian;

namespace Morpher.WebService.V3.Russian.Adaptor
{
    enum ERussianCase
    {
        Nominative, Genitive, Dative, Accusative, Instrumental, Prepositional, Locative
    }

    class SlavicParadigm : ISlavicParadigm
    {
        protected static string Convert(ERussianCase russianCase)
        {
            return ((int)russianCase).ToString();
        }

        string ISlavicParadigm.Nominative => Convert(ERussianCase.Nominative);

        string ISlavicParadigm.Genitive => Convert(ERussianCase.Genitive);

        string ISlavicParadigm.Dative => Convert(ERussianCase.Dative);

        string ISlavicParadigm.Accusative => Convert(ERussianCase.Accusative);

        string ISlavicParadigm.Instrumental => Convert(ERussianCase.Instrumental);

        string ISlavicParadigm.Prepositional => Convert(ERussianCase.Prepositional);
    }    

    class Paradigm : SlavicParadigm, IParadigm
    {
        string IParadigm.Locative => Convert(ERussianCase.Locative);
    }
}
