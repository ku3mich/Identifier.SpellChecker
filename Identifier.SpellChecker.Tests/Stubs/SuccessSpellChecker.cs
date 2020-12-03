using System.Collections.Generic;
using SpellChecker;

namespace Identifier.SpellChecker.Tests
{
    public class SuccessSpellChecker : SerializableSpellChecker, ISpellChecker
    {
        public static ISpellChecker Instance { get; } = new SuccessSpellChecker();

        public bool Check(string word) => true;

        public IEnumerable<string> Suggest(string word)
        {
            yield return word;
        }
    }
}
