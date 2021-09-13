using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Roslyn.Verifiers.CSharp
{
    public class DefaultDiagnoseParseOptionsFactory : IParseOptionsFactory
    {
        private readonly ILanguageVersionProvider VersionProvider;

        public DefaultDiagnoseParseOptionsFactory(ILanguageVersionProvider versionProvider)
        {
            VersionProvider = versionProvider;
        }

        public ParseOptions CreateParseOptions() 
            => new CSharpParseOptions(VersionProvider.LanguageVersion, DocumentationMode.Diagnose);
    }
}
