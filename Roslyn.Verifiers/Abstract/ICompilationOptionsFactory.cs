using Microsoft.CodeAnalysis;

namespace Roslyn.Verifiers
{
    public interface ICompilationOptionsFactory
    {
        CompilationOptions CreateCompilationOptions();
    }
}
