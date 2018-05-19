using System;
using Morpher.Russian;

namespace Morpher.WebService.V3.Russian.Adaptor
{
    public class Declension : IDeclension
    {
        private readonly Client _client;

        public Declension(Client client)
        {            
            _client = client;
        }

        public IParse Parse(string s, ParseArgs args = null)
        {
            DeclensionFlags flags = ConvertToDeclensionFlags(args);            
            var parse = _client.Parse(s, flags);            
            return new Parse(parse);
        }
        
        static private DeclensionFlags ConvertToDeclensionFlags(ParseArgs args)
        {
            if (args == null)
                return 0;

             DeclensionFlags result = 0;

            

            switch (args.Category)
            {
                case null:
                    break;
                case Category.Name:
                    result |= DeclensionFlags.Name;
                    break;
                case Category.Other:
                    result |= DeclensionFlags.Common;
                    break;
            }
            
            result |= args.Gender.Get(new GenderConverter());            

            switch (args.IsAnimate)
            {
                case null:
                    break;
                case true:
                    result |= DeclensionFlags.Animate;
                    break;
                case false:
                    result |= DeclensionFlags.Inanimate;
                    break;
            }

            return result;
        }
    }


    class GenderConverter : IGenderParadigm<DeclensionFlags>
    {
        public DeclensionFlags Masculine => DeclensionFlags.Masculine;

        public DeclensionFlags Feminine => DeclensionFlags.Feminine;

        public DeclensionFlags Neuter => DeclensionFlags.Neuter;

        public DeclensionFlags Plural => DeclensionFlags.Plural;
    }

   
    class Parse : IParse
    {        
        private readonly DeclensionResult _declensionResult;        

        public Parse(DeclensionResult declensionResult)
        {
            _declensionResult = declensionResult;            
        }               

        IParadigm IParse.Plural => new PluralParadigm(_declensionResult.Plural);     

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
                    case Gender.Neuter:
                        return Morpher.Gender.Neuter;
                    case Gender.Plural:
                        return Morpher.Gender.Plural;
                    default:
                        return null;                        
                }
            }
        }

        bool IParse.IsAnimate => throw new NotImplementedException();       
        string IParse.Paucal => throw new NotImplementedException();
        string IParadigm.Locative => null;//maybe _declensionResult.Where?
        string ISlavicParadigm.Nominative => _declensionResult.Nominative;
        string ISlavicParadigm.Genitive => _declensionResult.Genitive;
        string ISlavicParadigm.Dative => _declensionResult.Dative;
        string ISlavicParadigm.Accusative => _declensionResult.Accusative;
        string ISlavicParadigm.Instrumental => _declensionResult.Instrumental;
        string ISlavicParadigm.Prepositional => _declensionResult.Prepositional;
    }


    class PluralParadigm : IParadigm
    {
        readonly DeclensionForms _declensionForms;

        public PluralParadigm(DeclensionForms declensionForms)
        {
            _declensionForms = declensionForms;
        }

        string IParadigm.Locative => null;//??
        string ISlavicParadigm.Nominative => _declensionForms.Nominative;
        string ISlavicParadigm.Genitive => _declensionForms.Genitive;
        string ISlavicParadigm.Dative => _declensionForms.Dative;
        string ISlavicParadigm.Accusative => _declensionForms.Accusative;
        string ISlavicParadigm.Instrumental => _declensionForms.Instrumental;
        string ISlavicParadigm.Prepositional => _declensionForms.Prepositional;
    }
}