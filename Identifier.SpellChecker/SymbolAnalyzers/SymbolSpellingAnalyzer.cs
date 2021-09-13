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
            ISymbol symbol = context.Symbol;
            foreach (string identifier in GetSymbols(symbol))
            {
                IdentifierCheckResult checkResult = Speller.Check(identifier);
                if (checkResult.IsCorrect)
                    continue;

                IEnumerable<string> inncorrectParts = checkResult
                    .Parts
                    .Where(s => !s.IsCorrect)
                    .Select(s => s.Part.Value);

                string incorrectPartsArg = string.Join(", ", inncorrectParts);

                Diagnostic diagnostic = Diagnostic.Create(
                    IdentifierSpellCheckerAnalyzer.Rule,
                    symbol.Locations[0],
                    identifier,
                    incorrectPartsArg);

                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}
