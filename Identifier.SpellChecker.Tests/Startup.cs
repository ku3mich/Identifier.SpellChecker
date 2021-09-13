using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Roslyn.Verifiers;
using SpellChecker;
using System;
using Xunit.Extensions.Essentials;

namespace Identifier.SpellChecker.Tests
{
    public class Startup
    {
        public void ConfigureHost(IHostBuilder hostBuilder) =>
            hostBuilder
                .ConfigureXunitEssentials()
                .ConfigureVerifiersEssentials()
                .ConfigureServices(s => s
                    .AddTransient<IdentifierSpellCheckerAnalyzer>()
                    .AddTransient<SpellCheckerAnalyzerTest>()
                    .AddSingleton<IdentifierSpeller>()
                    .AddSingleton<TextFileProvider>()
                    .AddSingleton(WordListChecker.Instance)
                    .AddSingleton<Func<SpellCheckerAnalyzerTest>>(s => () => s.GetRequiredService<SpellCheckerAnalyzerTest>())
                    .AddTransient<SpellCheckerAnalyzerVerifier>());
    }
}
