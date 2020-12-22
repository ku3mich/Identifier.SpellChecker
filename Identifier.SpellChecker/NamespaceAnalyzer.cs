using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace Identifier.SpellChecker
{
    public class NamespaceAnalyzer : SymbolSpellingAnalyzer
    {
        public override SymbolKind Kind { get; } = SymbolKind.Namespace;

        public NamespaceAnalyzer(IIdentifierSpeller speller) : base(speller)
        {
        }

        public override IEnumerable<string> GetSymbols(ISymbol symbol)
        {
            INamespaceSymbol localSymbol = (INamespaceSymbol)symbol;
            string[] names = localSymbol.Name.Split(new[] { '.' });
            foreach (string s in names)
            {
                yield return s;
            }
        }
    }
}
