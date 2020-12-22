using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using SpellChecker;

namespace Identifier.SpellChecker
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class IdentifierSpellCheckerAnalyzer : DiagnosticAnalyzer
    {
        static IdentifierSpellCheckerAnalyzer()
        {
            Binder.Bind();
            NLog.LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration(Path.Combine(Binder.LocalFolder, "nlog.config"));
        }

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

        protected IServiceProvider ServiceProvider => _ServiceProvider.Value;
        readonly Lazy<IServiceProvider> _ServiceProvider;

        readonly Lazy<ILogger<IdentifierSpellCheckerAnalyzer>> _Logger;
        ILogger<IdentifierSpellCheckerAnalyzer> Logger => _Logger.Value;

        readonly List<ISpellChecker> CustomCheckers = new List<ISpellChecker>();

        public IdentifierSpellCheckerAnalyzer()
        {
            _ServiceProvider = new Lazy<IServiceProvider>(() =>
            {
                ServiceCollection services = new ServiceCollection();
                ConfigureServices(services);
                return services.BuildServiceProvider();
            }, true);

            _Logger = new Lazy<ILogger<IdentifierSpellCheckerAnalyzer>>(
                () => ServiceProvider.GetRequiredService<ILogger<IdentifierSpellCheckerAnalyzer>>(),
                true);
        }

        protected virtual void ConfigureServices(IServiceCollection services)
        {
            ConfigureLogger(services);
            ConfigureSpeller(services);
            services.AddTransient<IIdentifierSpeller, IdentifierSpeller>();
            services.AddSingleton<IEnumerable<ISpellChecker>>(CustomCheckers);
            IEnumerable<Type> analyzerTypes = typeof(IdentifierSpellCheckerAnalyzer)
                .Assembly
                .GetTypes()
                .Where(s => s.IsClass && !s.IsAbstract && typeof(ISymbolAnalyzer).IsAssignableFrom(s));

            foreach (Type analyzerType in analyzerTypes)
            {
                services.AddSingleton(typeof(ISymbolAnalyzer), analyzerType);
            }
        }

        protected virtual void ConfigureLogger(IServiceCollection services)
        {
            services.AddLogging(s =>
           {
               s.SetMinimumLevel(LogLevel.Trace);
               s.AddNLog();
           });
        }

        protected virtual void ConfigureSpeller(IServiceCollection services)
        {
            services.AddSingleton(WordListChecker.Instance);
        }

        public override void Initialize(AnalysisContext context)
        {
            Logger.LogTrace("Initialize");
            context.RegisterCompilationStartAction(ReloadCustomDictionaries);

            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            // context.EnableConcurrentExecution();

            IEnumerable<ISymbolAnalyzer> symbolAnalyzers = ServiceProvider.GetRequiredService<IEnumerable<ISymbolAnalyzer>>();

            foreach (ISymbolAnalyzer analyzer in symbolAnalyzers)
            {
                Logger.LogTrace($"registering analyzer: {analyzer.GetType().Name} for {analyzer.Kind}");
                context.RegisterSymbolAction(analyzer.Analyze, analyzer.Kind);
            }
        }

#pragma warning disable RS1012 // Start action has no registered actions
        private void ReloadCustomDictionaries(CompilationStartAnalysisContext context)
#pragma warning restore RS1012 // Start action has no registered actions
        {
            CustomCheckers.Clear();

            ImmutableArray<AdditionalText> additionalFiles = context.Options.AdditionalFiles;
            IEnumerable<FileWordListChecker> checkers = additionalFiles
                .Where(s => Path.GetExtension(s.Path) == ".spell")
                .Select(s =>
                {
                    try
                    {
                        FileWordListChecker c = new FileWordListChecker(s.Path);
                        return c;
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(ex, "while loading dictionary: {0}", s);
                        return null;
                    }
                })
                .Where(s => s != null);

            CustomCheckers.AddRange(checkers);
        }
    }
}
