using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.CSharp;
using Roslyn.Verifiers.CSharp;

namespace Roslyn.Verifiers
{
    public abstract class CSharpAnalyzerTest<TAnalyzer, TVerifier> : AnalyzerTest<TVerifier>
        where TAnalyzer : DiagnosticAnalyzer
        where TVerifier : IVerifier, new()
    {
        private readonly TAnalyzer Analyzer;
        private readonly ILanguageVersionProvider VersionProvider;
        private readonly ICompilationOptionsFactory CompilationOptionsFactory;

        protected CSharpAnalyzerTest(TAnalyzer analyzer, ILanguageVersionProvider versionProvider, ICompilationOptionsFactory compilationOptionsFactory)
        {
            Analyzer = analyzer;
            VersionProvider = versionProvider;
            CompilationOptionsFactory = compilationOptionsFactory;
        }

        protected override string DefaultFileExt => "cs";

        public override string Language => "C#";

        protected override CompilationOptions CreateCompilationOptions()
            => CompilationOptionsFactory.CreateCompilationOptions();

        protected override ParseOptions CreateParseOptions() =>
                new CSharpParseOptions(VersionProvider.LanguageVersion, DocumentationMode.Diagnose);

        protected override IEnumerable<DiagnosticAnalyzer> GetDiagnosticAnalyzers()
        {
            yield return Analyzer;
        }
    }
}
