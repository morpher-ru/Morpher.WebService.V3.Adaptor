using System;
using Morpher.Russian;


namespace Morpher.WebService.V3.Russian.Adaptor
{
    public class NumberSpelling : INumberSpelling
    {
        private MorpherClient _morpher;
        private Client _client;

        public NumberSpelling(Guid? token = null, string url = null)
        {
            _morpher = new MorpherClient(token, url);
            _client = _morpher.Russian;
        }

        public string Spell(decimal n, ref string unit, ICase<IParadigm> @case)
        {           
            if (string.IsNullOrEmpty(unit))
                return null;

            var spellResult = _client.Spell(n, unit);

            ERussianCase russianCase = GetCase(@case.Get(new Paradigm()));            

            string result;
            switch (russianCase)
            {
                case ERussianCase.Nominative:
                    {
                        result = spellResult.NumberDeclension.Nominative;
                        unit = spellResult.UnitDeclension.Nominative;
                    }
                    break;
                case ERussianCase.Genitive:
                    {
                        result = spellResult.NumberDeclension.Genitive;
                        unit = spellResult.UnitDeclension.Genitive;
                    }
                    break;
                case ERussianCase.Dative:
                    {
                        result = spellResult.NumberDeclension.Dative;
                        unit = spellResult.UnitDeclension.Dative;
                    }
                    break;
                case ERussianCase.Accusative:
                    {
                        result = spellResult.NumberDeclension.Accusative;
                        unit = spellResult.UnitDeclension.Accusative;
                    }
                    break;
                case ERussianCase.Instrumental:
                    {
                        result = spellResult.NumberDeclension.Instrumental;
                        unit = spellResult.UnitDeclension.Instrumental;
                    }
                    break;
                case ERussianCase.Prepositional:
                    {
                        result = spellResult.NumberDeclension.Prepositional;
                        unit = spellResult.UnitDeclension.Prepositional;
                    }
                    break;
                case ERussianCase.Locative:
                    {                        
                        throw new NotImplementedException();
                    }                    
                default:
                    {
                        System.Diagnostics.Debug.Assert(false, "russianCase");
                        throw new NotImplementedException();
                    }
            };

            return result;
        }

        static ERussianCase GetCase(string caseNumber)
        {
            return (ERussianCase)int.Parse(caseNumber);
        }
    }
}
