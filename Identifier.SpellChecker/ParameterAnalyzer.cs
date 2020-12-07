using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace Identifier.SpellChecker
{
    public class ParameterAnalyzer : SymbolSpellingAnalyzer
    {
        public override SymbolKind Kind { get; } = SymbolKind.Parameter;

        public ParameterAnalyzer(IIdentifierSpeller speller) : base(speller)
        {
        }

        public override IEnumerable<string> GetSymbols(ISymbol symbol)
        {
            var s = (IParameterSymbol)symbol;
            yield return s.Name;
        }
    }
}
