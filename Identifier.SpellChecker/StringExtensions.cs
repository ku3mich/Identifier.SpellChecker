using System;
using System.Collections.Generic;
using System.Text;

namespace Identifier.SpellChecker
{
    public static class StringExtensions
    {
        private static PartType GetPartType(char ch)
        {
            if (char.IsLetter(ch))
                return PartType.Word;

            return PartType.Else;
        }

        public static IEnumerable<IdentifierPart> SplitCamelCase(this string identifier)
        {
            if (string.IsNullOrWhiteSpace(identifier))
                yield break;

            var sb = new StringBuilder();
            var partType = PartType.Empty;

            foreach (var ch in identifier)
            {
                var charType = GetPartType(ch);
                if (charType == partType && !char.IsUpper(ch))
                {
                    sb.Append(ch);
                    continue;
                }

                if (sb.Length > 0)
                {
                    yield return new IdentifierPart(sb.ToString(), partType);
                    sb.Clear();
                }

                sb.Append(ch);
                partType = charType;
            }

            if (sb.Length > 0)
                yield return new IdentifierPart(sb.ToString(), partType);
        }
    }
}
