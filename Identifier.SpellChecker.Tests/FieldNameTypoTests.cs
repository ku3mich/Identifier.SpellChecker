
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SpellChecker;
using Xunit;
using Xunit.Abstractions;
using Verifier = Identifier.SpellChecker.Tests.CSharpAnalyzerVerifier<Identifier.SpellChecker.Tests.IdentifierSpellCheckerTestedAnalyzer>;

namespace Identifier.SpellChecker.Tests
{
    public class FieldNameTypoTests : XunitContextBase
    {
        private readonly static string SampleCode = File.ReadAllText(PathResolver.Instance.Resolve("sample/field.typo.cs"));

        public FieldNameTypoTests(ITestOutputHelper output) : base(output, ".") { }

        [Theory]
        [MemberData(nameof(Fail))]
        public async Task Fails(string code, ISpellChecker checker)
        {
            Assert.NotNull(checker);
            await Verifier.VerifyAnalyzerAsync(code,
                Verifier.Diagnostic("ISC1000")
                .WithLocation(0)
                .WithArguments("Asdgcbn", "Asdgcbn"));
        }

        public static IEnumerable<object[]> Fail => new List<object[]> { new object[] { SampleCode, RealSpellChecker.Instance } };
    }
}
