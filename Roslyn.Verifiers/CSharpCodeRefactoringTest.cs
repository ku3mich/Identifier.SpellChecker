using System;
using System.Collections.Generic;
using System.Collections.Immutable;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeRefactorings;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;

namespace Roslyn.Verifiers.CSharp
{
    public abstract class CSharpCodeRefactoringTest<TCodeRefactoring, TVerifier> : CodeRefactoringTest<TVerifier>
        where TCodeRefactoring : CodeRefactoringProvider
        where TVerifier : IVerifier, new()
    {
        protected readonly TCodeRefactoring CodeRefactoring;
        private readonly ILanguageVersionProvider LanguageVersionProvider;
        private readonly ICompilationOptionsFactory CompilationFactory;

        protected override string DefaultFileExt => "cs";

        public override string Language => "C#";

        public override Type SyntaxKindType => typeof(SyntaxKind);

        protected CSharpCodeRefactoringTest(
            TCodeRefactoring codeRefactoring,
            ILanguageVersionProvider languageVersionProvider,
            ICompilationOptionsFactory compilationFactory)
        {
            CodeRefactoring = codeRefactoring;
            LanguageVersionProvider = languageVersionProvider;
            CompilationFactory = compilationFactory;
        }

        protected override IEnumerable<CodeRefactoringProvider> GetCodeRefactoringProviders()
        {
            yield return CodeRefactoring;
        }

        protected override CompilationOptions CreateCompilationOptions() => CompilationFactory.CreateCompilationOptions();

        protected override ParseOptions CreateParseOptions() => new CSharpParseOptions(LanguageVersionProvider.LanguageVersion, DocumentationMode.Diagnose);
    }
}
