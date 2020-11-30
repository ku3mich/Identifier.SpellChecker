using System.Collections.Generic;

namespace SpellChecker
{
    public interface ISpellChecker
    {
        IEnumerable<string> Suggest(string word);
        bool Check(string word);
    }
}
