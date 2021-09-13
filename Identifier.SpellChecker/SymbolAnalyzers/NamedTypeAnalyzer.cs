using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace Identifier.SpellChecker
{
    public class NamedTypeAnalyzer : SymbolSpellingAnalyzer
    {
        public override SymbolKind Kind { get; } = SymbolKind.NamedType;

        public NamedTypeAnalyzer(IIdentifierSpeller speller) : base(speller)
        {
        }

        public override IEnumerable<string> GetSymbols(ISymbol symbol)
        {
            yield return ((INamedTypeSymbol)symbol).Name;
        }
    }
}
