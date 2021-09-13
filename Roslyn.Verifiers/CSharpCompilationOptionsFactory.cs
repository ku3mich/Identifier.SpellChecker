using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Roslyn.Verifiers
{
    public class CSharpCompilationOptionsFactory : ICompilationOptionsFactory
    {
        public CompilationOptions CreateCompilationOptions() => new CSharpCompilationOptions(
                OutputKind.DynamicallyLinkedLibrary,
                null,
                null,
                null,
                null,
                OptimizationLevel.Debug,
                checkOverflow: false,
                allowUnsafe: true,
                null,
                null,
                default,
                null,
                Platform.AnyCpu,
                ReportDiagnostic.Default,
                4, // Warning Level
                null,
                concurrentBuild: true,
                null,
                null,
                null,
                null,
                null);
    }
}
