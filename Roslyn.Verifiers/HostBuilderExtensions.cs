using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Roslyn.Verifiers.CSharp;

namespace Roslyn.Verifiers
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder ConfigureVerifiersEssentials(this IHostBuilder hostBuilder) =>
            hostBuilder.ConfigureServices((h, s) => s
                .AddSingleton<ILanguageVersionProvider, LanguageVersionProvider>()
                .AddSingleton<ISolutionTransformProvider, DefaultCompillerSetup>()
                .AddSingleton<ICompilationOptionsFactory, CSharpCompilationOptionsFactory>()
                .AddSingleton<IParseOptionsFactory, DefaultDiagnoseParseOptionsFactory>());
    }
}
