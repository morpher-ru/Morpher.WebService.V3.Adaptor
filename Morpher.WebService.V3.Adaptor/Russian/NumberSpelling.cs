using System;
using Morpher.Russian;


namespace Morpher.WebService.V3.Russian.Adaptor
{
    public class NumberSpelling : INumberSpelling
    {
        private readonly Client _client;

        public NumberSpelling(Client client)
        {
            _client = client;
        }

        public string Spell(decimal n, ref string unit, ICase<IParadigm> @case)
        {
            if (string.IsNullOrEmpty(unit))
                return null;

            var spellResult = _client.Spell(n, unit);

            string[] result = @case.Get(new Paradigm(spellResult))
                .Split(Paradigm.Separator);

            unit = result[1];
            return result[0];
        }
    }
}
