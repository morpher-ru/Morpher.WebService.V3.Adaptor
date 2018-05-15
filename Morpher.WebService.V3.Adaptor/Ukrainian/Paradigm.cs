using Morpher.Ukrainian;

namespace Morpher.WebService.V3.Ukrainian.Adaptor
{
    enum EUkrainianCase
    {
        Nominative, Genitive, Dative, Accusative, Instrumental, Prepositional, Vocative
    }

    class SlavicParadigm : ISlavicParadigm
    {
        protected static string Convert(EUkrainianCase ukrainianCase)
        {
            return ((int)ukrainianCase).ToString();
        }

        string ISlavicParadigm.Nominative => Convert(EUkrainianCase.Nominative);

        string ISlavicParadigm.Genitive => Convert(EUkrainianCase.Genitive);

        string ISlavicParadigm.Dative => Convert(EUkrainianCase.Dative);

        string ISlavicParadigm.Accusative => Convert(EUkrainianCase.Accusative);

        string ISlavicParadigm.Instrumental => Convert(EUkrainianCase.Instrumental);

        string ISlavicParadigm.Prepositional => Convert(EUkrainianCase.Prepositional);
    }

    class Paradigm : SlavicParadigm, IParadigm
    {
        string IParadigm.Vocative => Convert(EUkrainianCase.Vocative);
    }
}
