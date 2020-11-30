using System.Collections.Generic;
using SpellChecker;

namespace Identifier.SpellChecker.Tests
{
    public class RealSpellChecker : SerializableSpellChecker, ISpellChecker
    {
        public static ISpellChecker Instance { get; } = new RealSpellChecker();

        public bool Check(string word) => WordListChecker.Instance.Check(word);
        public IEnumerable<string> Suggest(string word) => WordListChecker.Instance.Suggest(word);
    }
}
