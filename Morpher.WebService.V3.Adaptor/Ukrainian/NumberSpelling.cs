using System;
using Morpher.Ukrainian;


namespace Morpher.WebService.V3.Ukrainian.Adaptor
{
    public class NumberSpelling : INumberSpelling
    {
        private MorpherClient _morpher;
        private Client _client;

        public NumberSpelling(Guid? token = null, string url = null)
        {
            _morpher = new MorpherClient(token, url);
            _client = _morpher.Ukrainian;
        }

        public string Spell(decimal n, ref string unit, ICase<IParadigm> @case)
        {
            if (string.IsNullOrEmpty(unit))
                return null;

            var spellResult = _client.Spell(n, unit);

            EUkrainianCase ukrainianCase = GetCase(@case.Get(new Paradigm()));

            string result;
            switch (ukrainianCase)
            {
                case EUkrainianCase.Nominative:
                    {
                        result = spellResult.NumberDeclension.Nominative;
                        unit = spellResult.UnitDeclension.Nominative;
                    }
                    break;
                case EUkrainianCase.Genitive:
                    {
                        result = spellResult.NumberDeclension.Genitive;
                        unit = spellResult.UnitDeclension.Genitive;
                    }
                    break;
                case EUkrainianCase.Dative:
                    {
                        result = spellResult.NumberDeclension.Dative;
                        unit = spellResult.UnitDeclension.Dative;
                    }
                    break;
                case EUkrainianCase.Accusative:
                    {
                        result = spellResult.NumberDeclension.Accusative;
                        unit = spellResult.UnitDeclension.Accusative;
                    }
                    break;
                case EUkrainianCase.Instrumental:
                    {
                        result = spellResult.NumberDeclension.Instrumental;
                        unit = spellResult.UnitDeclension.Instrumental;
                    }
                    break;
                case EUkrainianCase.Prepositional:
                    {
                        result = spellResult.NumberDeclension.Prepositional;
                        unit = spellResult.UnitDeclension.Prepositional;
                    }
                    break;
                case EUkrainianCase.Vocative:
                    {
                        result = spellResult.NumberDeclension.Vocative;
                        unit = spellResult.UnitDeclension.Vocative;
                    }
                    break;
                default:
                    {
                        System.Diagnostics.Debug.Assert(false, "ukrainianCase");
                        throw new NotImplementedException();
                    }                    
            };

            return result;
        }

        static EUkrainianCase GetCase(string caseNumber)
        {
            return (EUkrainianCase)int.Parse(caseNumber);
        }
    }
}
