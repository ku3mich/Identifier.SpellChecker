using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Testing.Verifiers;
using Roslyn.Verifiers.CSharp;

namespace Roslyn.Verifiers.Xunit
{
    public class CSharpAnalyzerTest<TAnalyzer> : CSharpAnalyzerTest<TAnalyzer, XUnitVerifier>
        where TAnalyzer : DiagnosticAnalyzer
    {
        public CSharpAnalyzerTest(
            TAnalyzer analyzer,
            ISolutionTransformProvider transformProvider,
            ILanguageVersionProvider versionProvider,
            ICompilationOptionsFactory compilationOptionsFactory) : base(analyzer, versionProvider, compilationOptionsFactory)
        {
            SolutionTransforms.AddRange(transformProvider.SolutionTransforms);
        }
    }
}
