using System.IO;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Identifier.SpellChecker.Tests
{
    public class ClassNameTests : SpellingTest
    {
        public ClassNameTests(ITestOutputHelper output, TextFileProvider provider, SpellCheckerAnalyzerVerifier verifier) : base(output, provider, verifier)
        {
        }

        [Fact]
        public async Task Correct()
        {
            var code = await ProvideTextAsync("sample/classname.correct.cs");
            await Verifier.VerifyAnalyzerAsync(code);
        }

        [Fact]
        public async Task Typo()
        {
            var code = await ProvideTextAsync("sample/classname.typo.cs");
            await Verifier.VerifyAnalyzerAsync(code,
                Verifier.Diagnostic("ISC1000")
                .WithLocation(0)
                .WithArguments("Aplication", "Aplication"));
        }
    }
}
