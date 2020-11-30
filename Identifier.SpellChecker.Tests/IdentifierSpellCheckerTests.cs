
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SpellChecker;
using Xunit;
using Xunit.Abstractions;
using Verifier = Identifier.SpellChecker.Tests.CSharpAnalyzerVerifier<Identifier.SpellChecker.Tests.IdentifierSpellCheckerTestedAnalyzer>;

namespace Identifier.SpellChecker.Tests
{

    public class IdentifierSpellCheckerTests : XunitContextBase
    {
        public IdentifierSpellCheckerTests(ITestOutputHelper output) : base(output, ".")
        {
        }

        public ITestOutputHelper TestOutputHelper => Output;

        public ISpellChecker SpellChecker { get; } = new FailSpellChecker();

        [Theory]
        [MemberData(nameof(Success))]
        public async Task Successed(string code, ISpellChecker checker)
        {
            Assert.NotNull(checker);
            await Verifier.VerifyAnalyzerAsync(code);
        }

        [Theory]
        [MemberData(nameof(Fail))]
        public async Task Failed(string code, ISpellChecker checker)
        {
            Assert.NotNull(checker);
            await Verifier.VerifyAnalyzerAsync(code, Verifier.Diagnostic("ISC1000").WithLocation(0).WithArguments("Aplication"));
        }

        [Theory]
        [MemberData(nameof(Real))]
        public async Task RealSpeller(string code, ISpellChecker checker)
        {
            Assert.NotNull(checker);
            await Verifier.VerifyAnalyzerAsync(code, Verifier.Diagnostic("ISC1000").WithLocation(0).WithArguments("Aplication"));
        }

        private static string SampleCode = File.ReadAllText(PathResolver.Instance.Resolve("sample/classname.typo.cs"));

        public static IEnumerable<object[]> Success =>
           new List<object[]>
           {
                new object[] {
                    SampleCode,
                    SuccessSpellChecker.Instance
                } };

        public static IEnumerable<object[]> Fail =>
            new List<object[]>
           {
                new object[] {
                    SampleCode,
                    FailSpellChecker.Instance
                }
           };

        public static IEnumerable<object[]> Real =>
            new List<object[]>
           {
                new object[] {
                    SampleCode,
                    RealSpellChecker.Instance
                }
           };
    }
}
