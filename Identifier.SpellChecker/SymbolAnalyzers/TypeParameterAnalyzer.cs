using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace Identifier.SpellChecker
{
    public class TypeParameterAnalyzer : SymbolSpellingAnalyzer
    {
        public override SymbolKind Kind { get; } = SymbolKind.TypeParameter;

        public TypeParameterAnalyzer(IIdentifierSpeller speller) : base(speller)
        {
        }

        public override IEnumerable<string> GetSymbols(ISymbol symbol)
        {
            ITypeParameterSymbol s = (ITypeParameterSymbol)symbol;
            yield return s.Name;
        }
    }
}
