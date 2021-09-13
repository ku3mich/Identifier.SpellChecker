using Microsoft.CodeAnalysis.CSharp;

namespace Roslyn.Verifiers.CSharp
{
    public interface ILanguageVersionProvider
    {
        LanguageVersion LanguageVersion { get; }
    }
}
