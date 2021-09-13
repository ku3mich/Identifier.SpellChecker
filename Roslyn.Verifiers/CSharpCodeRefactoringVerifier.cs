using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CodeRefactorings;
using Microsoft.CodeAnalysis.Testing;

namespace Roslyn.Verifiers.CSharp
{
    public abstract class CSharpCodeRefactoringVerifier<TCodeRefactoring, TVerifier>
        where TCodeRefactoring : CodeRefactoringProvider
        where TVerifier : IVerifier, new()
    {
        protected readonly CSharpCodeRefactoringTest<TCodeRefactoring, TVerifier> Test;

        protected CSharpCodeRefactoringVerifier(CSharpCodeRefactoringTest<TCodeRefactoring, TVerifier> test)
        {
            Test = test;
        }

        public async Task VerifyRefactoringAsync(string source, string fixedSource)
        {
            await VerifyRefactoringAsync(source, DiagnosticResult.EmptyDiagnosticResults, fixedSource);
        }

        public async Task VerifyRefactoringAsync(string source, DiagnosticResult expected, string fixedSource)
        {
            await VerifyRefactoringAsync(source, new[] { expected }, fixedSource);
        }

        public async Task VerifyRefactoringAsync(string source, DiagnosticResult[] expected, string fixedSource)
        {
            Test.TestCode = source;
            Test.FixedCode = fixedSource;

            Test.ExpectedDiagnostics.AddRange(expected);
            await Test.RunAsync(CancellationToken.None);
        }
    }
}
