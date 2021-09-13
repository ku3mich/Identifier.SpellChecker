using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Identifier.SpellChecker
{
    public interface ISymbolAnalyzer
    {
        SymbolKind Kind { get; }
        void Analyze(SymbolAnalysisContext context);
    }
}
