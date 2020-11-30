using System.Collections.Generic;
using System.Linq;
using SpellChecker;

namespace Identifier.SpellChecker.Tests
{
    public class FailSpellChecker : SerializableSpellChecker, ISpellChecker
    {
        public static ISpellChecker Instance { get; } = new FailSpellChecker();

        public bool Check(string word) => false;
        public IEnumerable<string> Suggest(string word) => Enumerable.Empty<string>();
    }
}
