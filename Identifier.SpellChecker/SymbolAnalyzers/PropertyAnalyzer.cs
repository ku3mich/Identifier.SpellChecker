using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace Identifier.SpellChecker
{
    public class PropertyAnalyzer : SymbolSpellingAnalyzer
    {
        public override SymbolKind Kind { get; } = SymbolKind.Property;

        public PropertyAnalyzer(IIdentifierSpeller speller) : base(speller)
        {
        }

        public override IEnumerable<string> GetSymbols(ISymbol symbol)
        {
            IPropertySymbol s = (IPropertySymbol)symbol;
            yield return s.Name;
        }
    }
}
