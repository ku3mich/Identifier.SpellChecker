using Xunit.Abstractions;

namespace Identifier.SpellChecker.Tests
{
    public abstract class SpellingTest : TextFileTest
    {
        protected readonly SpellCheckerAnalyzerVerifier Verifier;

        protected SpellingTest(ITestOutputHelper output, TextFileProvider provider, SpellCheckerAnalyzerVerifier verifier) : base(output, provider)
        {
            Verifier = verifier;
        }
    }
}
