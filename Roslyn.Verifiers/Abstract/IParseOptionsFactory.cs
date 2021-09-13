
using Microsoft.CodeAnalysis;

namespace Roslyn.Verifiers
{
    public interface IParseOptionsFactory
    {
        ParseOptions CreateParseOptions();
    }
}
