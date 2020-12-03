using System.Collections.Generic;
using System.IO;
using WeCantSpell.Hunspell;

namespace SpellChecker
{
    public class FileWordListChecker : ISpellChecker
    {
        private readonly WordList Instance;
        private readonly string FileName;

        public FileWordListChecker(string fileName)
        {
            Instance = WordList.CreateFromWords(EnumerateWordsFromFile(fileName));
            FileName = fileName;
        }

        private static IEnumerable<string> EnumerateWordsFromFile(string fileName)
        {
            var lines = File.ReadAllLines(fileName);
            foreach (var line in lines)
            {
                foreach (var word in line.Split(new[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries))
                {
                    yield return word.ToLowerInvariant();
                }
            }
        }

        public bool Check(string word) => Instance.Check(word?.ToLowerInvariant());
        public IEnumerable<string> Suggest(string word) => Instance.Suggest(word?.ToLowerInvariant());

        public override string ToString() => $"[FileWordListChecker: {FileName}]";
    }
}

