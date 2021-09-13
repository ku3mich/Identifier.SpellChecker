using Roslyn.Verifiers;
using Roslyn.Verifiers.CSharp;
using Roslyn.Verifiers.Xunit;

namespace Identifier.SpellChecker.Tests
{
    public class SpellCheckerAnalyzerTest : CSharpAnalyzerTest<IdentifierSpellCheckerAnalyzer>
    {
        public SpellCheckerAnalyzerTest(
            IdentifierSpellCheckerAnalyzer analyzer,
            ISolutionTransformProvider transformProvider,
            ILanguageVersionProvider versionProvider,
            ICompilationOptionsFactory compilationOptionsFactory) : base(analyzer, transformProvider, versionProvider, compilationOptionsFactory)
        {
        }
    }
}
