using System.Linq;
using Microsoft.CodeAnalysis;

namespace Identifier.SpellChecker
{
    public readonly struct IdentifierCheckResult
    {
        public readonly string Identifier;
        public readonly bool IsCorrect;
        public readonly CheckedPart[] Parts;

        public IdentifierCheckResult(string identifier, CheckedPart[] parts)
        {
            Identifier = identifier;
            Parts = parts;
            IsCorrect = !parts.Any(s => !s.IsCorrect);
        }
    }
}
