using System;
using Morpher.Ukrainian;

namespace Morpher.WebService.V3.Ukrainian.Adapter
{
    public class Declension : IDeclension
    {
        private MorpherClient _morpher;
        private Client _client;

        public Declension(Guid? token = null, string url = null)
        {            
            _morpher = new MorpherClient(token, url);
            _client = _morpher.Ukrainian;
        }

        public IParse Parse(string s, ParseArgs args = null)
        {            
            var parse = _client.Parse(s);
            return new Parse(parse);
        }
    }


    class Parse : IParse
    {
        readonly DeclensionResult _declensionResult;

        public Parse(DeclensionResult declensionResult)
        {
            this._declensionResult = declensionResult;
        }

        Morpher.Gender IParse.Gender
        {            
            get
            {
                switch (_declensionResult.Gender)
                {
                    case Gender.Masculine:
                        return Morpher.Gender.Masculine;
                    case Gender.Feminine:
                        return Morpher.Gender.Feminine;
                    default:
                        return null;                        
                }
            }
        }
        
        string ISlavicParadigm.Nominative => _declensionResult.Nominative;
        string ISlavicParadigm.Genitive => _declensionResult.Genitive;
        string ISlavicParadigm.Dative => _declensionResult.Dative;
        string ISlavicParadigm.Accusative => _declensionResult.Accusative;
        string ISlavicParadigm.Instrumental => _declensionResult.Instrumental;
        string ISlavicParadigm.Prepositional => _declensionResult.Prepositional;
        string IParadigm.Vocative => _declensionResult.Vocative;
    }
}
