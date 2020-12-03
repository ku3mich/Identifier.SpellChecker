
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SpellChecker;
using Xunit;
using Xunit.Abstractions;
using Verifier = Identifier.SpellChecker.Tests.CSharpAnalyzerVerifier<Identifier.SpellChecker.Tests.IdentifierSpellCheckerTestedAnalyzer>;

namespace Identifier.SpellChecker.Tests
{
    public class ClassNameTypoTests : XunitContextBase
    {
        public ClassNameTypoTests(ITestOutputHelper output) : base(output, ".")
        {
        }

        [Theory]
        [MemberData(nameof(Success))]
        public async Task Successed(string code, ISpellChecker checker)
        {
            Assert.NotNull(checker);
            await Verifier.VerifyAnalyzerAsync(code);
        }

        [Theory]
        [MemberData(nameof(Fail))]
        [MemberData(nameof(Real))]
        public async Task Case(string code, ISpellChecker checker)
        {
            Assert.NotNull(checker);
            await Verifier.VerifyAnalyzerAsync(code,
                Verifier.Diagnostic("ISC1000")
                .WithLocation(0)
                .WithArguments("Aplication", "Aplication"));
        }

        private readonly static string SampleCode = File.ReadAllText(PathResolver.Instance.Resolve("sample/classname.typo.cs"));

        public static IEnumerable<object[]> Success => new object[][] { new object[] { SampleCode, SuccessSpellChecker.Instance } };
        public static IEnumerable<object[]> Fail => new object[][] { new object[] { SampleCode, FailSpellChecker.Instance } };
        public static IEnumerable<object[]> Real => new object[][] { new object[] { SampleCode, RealSpellChecker.Instance } };
    }
}
