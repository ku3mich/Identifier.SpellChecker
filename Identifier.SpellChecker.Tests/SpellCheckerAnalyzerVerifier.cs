
using System;
using Roslyn.Verifiers.Xunit;

namespace Identifier.SpellChecker.Tests
{
    public class SpellCheckerAnalyzerVerifier : AnalyzerVerifier<IdentifierSpellCheckerAnalyzer, SpellCheckerAnalyzerTest>
    {
        public SpellCheckerAnalyzerVerifier(IdentifierSpellCheckerAnalyzer analyzer, Func<SpellCheckerAnalyzerTest> test) : base(analyzer, test)
        {
        }
    }
}
