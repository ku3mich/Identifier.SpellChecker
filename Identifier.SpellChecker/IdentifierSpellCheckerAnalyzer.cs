using System;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SpellChecker;

namespace Identifier.SpellChecker
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class IdentifierSpellCheckerAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "ISC1000";

        // You can change these strings in the Resources.resx file. If you do not want your analyzer to be localize-able, you can use regular strings for Title and MessageFormat.
        // See https://github.com/dotnet/roslyn/blob/master/docs/analyzers/Localizing%20Analyzers.md for more on localization
        private static readonly LocalizableString Title = new LocalizableResourceString(
            nameof(Resources.AnalyzerTitle),
            Resources.ResourceManager,
            typeof(Resources));

        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(
            nameof(Resources.AnalyzerMessageFormat),
            Resources.ResourceManager,
            typeof(Resources));

        private static readonly LocalizableString Description = new LocalizableResourceString(
            nameof(Resources.AnalyzerDescription),
            Resources.ResourceManager,
            typeof(Resources));

        private const string Category = "Naming";

        public static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
            DiagnosticId,
            Title,
            MessageFormat,
            Category,
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true,
            description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create(Rule);

        protected IServiceProvider ServiceProvider;


        public IdentifierSpellCheckerAnalyzer()
        {
        }

        protected virtual void ConfigureServices(IServiceCollection services)
        {
            ConfigureLogger(services);
            ConfigureSpeller(services);
        }

        protected virtual void ConfigureLogger(IServiceCollection services)
        {
            services.AddLogging();
        }

        protected virtual void ConfigureSpeller(IServiceCollection services)
        {
            services.AddSingleton(WordListChecker.Instance);
        }

        ILogger<IdentifierSpellCheckerAnalyzer> Logger;
        ISpellChecker SpellChecker;

        public override void Initialize(AnalysisContext context)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            ServiceProvider = services.BuildServiceProvider();

            Logger = ServiceProvider.GetRequiredService<ILogger<IdentifierSpellCheckerAnalyzer>>();
            SpellChecker = ServiceProvider.GetRequiredService<ISpellChecker>();
            Logger.LogTrace("Initialize");

            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();

            // TODO: Consider registering other actions that act on syntax instead of or in addition to symbols
            // See https://github.com/dotnet/roslyn/blob/master/docs/analyzers/Analyzer%20Actions%20Semantics.md for more information
            context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.NamedType);
        }

        private void AnalyzeSymbol(SymbolAnalysisContext context)
        {
            var namedTypeSymbol = (INamedTypeSymbol)context.Symbol;
            var parts = namedTypeSymbol.Name.SplitCamelCase();
            var typo = parts
                .Where(s => s.Type == PartType.Word)
                .Any(s => !SpellChecker.Check(s.Part));

            if (!typo)
                return;

            var diagnostic = Diagnostic.Create(Rule, namedTypeSymbol.Locations[0], namedTypeSymbol.Name);
            context.ReportDiagnostic(diagnostic);
        }
    }
}
