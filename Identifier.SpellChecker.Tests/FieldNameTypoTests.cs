
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SpellChecker;
using Xunit;
using Xunit.Abstractions;

namespace Identifier.SpellChecker.Tests
{
    public class FieldNameTypoTests : SpellingTest
    {
        public FieldNameTypoTests(ITestOutputHelper output, TextFileProvider provider, SpellCheckerAnalyzerVerifier verifier) : base(output, provider, verifier)
        {
        }

        [Fact]
        public async Task Invalid_Name()
        {
            var code = await ProvideTextAsync("sample/field.typo.cs");
            await Verifier.VerifyAnalyzerAsync(code,
                Verifier.Diagnostic("ISC1000")
                .WithLocation(0)
                .WithArguments("Asdgcbn", "Asdgcbn"));
        }
    }
}
