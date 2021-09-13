using System.Threading.Tasks;
using Xunit;

namespace Identifier.SpellChecker.Tests
{
    public class IdentifierSpellCheckerFixUpUnitTest
    {
        [Fact]
        public async Task Empty()
        {
            string test = @"";
            //await VerifyCS.VerifyAnalyzerAsync(test);
        }


        [Theory(Skip = "not implemented yet")]
        [FileContent("sample/test.cs", "sample/test.e.cs")]
        public async Task FixpUp(string test, string fixtest)
        {
            /*Microsoft.CodeAnalysis.Testing.DiagnosticResult expected = VerifyCS
                .Diagnostic("ISC1000")
                .WithLocation(0)
                .WithArguments("TypeName");*/

            //await VerifyCS.VerifyCodeFixAsync(test, expected, fixtest);
        }
    }
}
