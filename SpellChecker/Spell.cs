using System;
using System.Reflection;
using WeCantSpell.Hunspell;

namespace SpellChecker
{
    public static class Spell
    {
        private static readonly Assembly Assembly = typeof(Spell).Assembly;
        static readonly Lazy<WordList> _dictionary = new Lazy<WordList>(LoadWordList, true);
        public static WordList Checker { get => _dictionary.Value; }

        static WordList LoadWordList()
        {
            using (System.IO.Stream dicStream = Assembly.GetManifestResourceStream("SpellChecker.en_US.dic"))
            using (System.IO.Stream affStream = Assembly.GetManifestResourceStream("SpellChecker.en_US.aff"))
            {
                return WordList.CreateFromStreams(dicStream, affStream);
            }
        }
    }
}
