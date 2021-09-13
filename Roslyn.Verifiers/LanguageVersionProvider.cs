using System;
using Microsoft.CodeAnalysis.CSharp;

namespace Roslyn.Verifiers.CSharp
{
    public class LanguageVersionProvider : ILanguageVersionProvider
    {
        public static LanguageVersion DefaultLanguageVersion { get; } =
            Enum.TryParse("Default", out LanguageVersion result)
            ? result
            : LanguageVersion.CSharp6;

        public LanguageVersion LanguageVersion => DefaultLanguageVersion;
    }
}
