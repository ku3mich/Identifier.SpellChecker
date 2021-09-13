using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace Identifier.SpellChecker
{
    public class MethodAnalyzer : SymbolSpellingAnalyzer
    {
        public override SymbolKind Kind { get; } = SymbolKind.Method;

        public MethodAnalyzer(IIdentifierSpeller speller) : base(speller)
        {
        }

        public override IEnumerable<string> GetSymbols(ISymbol symbol)
        {
            yield return ((IMethodSymbol)symbol).Name;
        }
    }
}
