﻿using System.Threading.Tasks;
using Xunit;

using VerifyCS = Identifier.SpellChecker.Tests.CSharpCodeFixVerifier<Identifier.SpellChecker.IdentifierSpellCheckerAnalyzer, Identifier.SpellChecker.IdentifierSpellCheckerCodeFixProvider>;

namespace Identifier.SpellChecker.Tests
{
    public class IdentifierSpellCheckerFixUpUnitTest
    {
        [Fact]
        public async Task Empty()
        {
            var test = @"";
            await VerifyCS.VerifyAnalyzerAsync(test);
        }


        [Theory(Skip = "not implemented yet")]
        [FileContent("sample/test.cs", "sample/test.e.cs")]
        public async Task FixpUp(string test, string fixtest)
        {
            var expected = VerifyCS.Diagnostic("ISC1000").WithLocation(0).WithArguments("TypeName");
            await VerifyCS.VerifyCodeFixAsync(test, expected, fixtest);
        }
    }
}
