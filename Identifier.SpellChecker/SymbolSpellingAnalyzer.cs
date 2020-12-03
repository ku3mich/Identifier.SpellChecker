using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Identifier.SpellChecker
{
    public abstract class SymbolSpellingAnalyzer : ISymbolAnalyzer
    {
        public abstract IEnumerable<string> GetSymbols(ISymbol symbol);

        public abstract SymbolKind Kind { get; }

        private readonly IIdentifierSpeller Speller;

        protected SymbolSpellingAnalyzer(IIdentifierSpeller speller)
        {
            Speller = speller;
        }

        public void Analyze(SymbolAnalysisContext context)
        {
            var symbol = context.Symbol;
            foreach (var identifier in GetSymbols(symbol))
            {
                var checkResult = Speller.Check(identifier);
                if (checkResult.IsCorrect)
                    continue;

                var inncorrectParts = checkResult
                    .Parts
                    .Where(s => !s.IsCorrect)
                    .Select(s => s.Part.Value);

                var incorrectPartsArg = string.Join(", ", inncorrectParts);

                var diagnostic = Diagnostic.Create(
                    IdentifierSpellCheckerAnalyzer.Rule,
                    symbol.Locations[0],
                    identifier,
                    incorrectPartsArg);

                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}
