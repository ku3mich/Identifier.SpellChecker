using Microsoft.CodeAnalysis.CodeRefactorings;
using Microsoft.CodeAnalysis.Testing.Verifiers;
using Roslyn.Verifiers.CSharp;

namespace Roslyn.Verifiers.Xunit
{
    public class CSharpCodeRefactoringTest<TCodeRefactoring> : CSharpCodeRefactoringTest<TCodeRefactoring, XUnitVerifier>
        where TCodeRefactoring : CodeRefactoringProvider
    {
        public CSharpCodeRefactoringTest(
            TCodeRefactoring codeRefactoring,
            ISolutionTransformProvider transformProvider,
            ILanguageVersionProvider languageVersionProvider,
            ICompilationOptionsFactory compilationFactory) : base(codeRefactoring, languageVersionProvider, compilationFactory)
        {
            SolutionTransforms.AddRange(transformProvider.SolutionTransforms);
        }
    }
}
