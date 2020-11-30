using System.Collections.Generic;

namespace SpellChecker
{
    public class WordListChecker : ISpellChecker
    {
        public static ISpellChecker Instance { get; } = new WordListChecker();
        protected WordListChecker() { }
        public bool Check(string word) => Spell.Checker.Check(word);
        public IEnumerable<string> Suggest(string word) => Spell.Checker.Suggest(word);
    }
}
