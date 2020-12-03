using System;
using System.Collections.Generic;
using System.Linq;
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

        private static bool MaybeAbbr(this IdentifierPart prev) =>
            prev.Type == PartType.Word && prev.Value.Length == 1 && char.IsUpper(prev.Value[0]);

        public static List<IdentifierPart> Analyze(this IEnumerable<IdentifierPart> indetifierParts)
        {
            var parts = indetifierParts.GetEnumerator();
            var result = new List<IdentifierPart>();

            if (!parts.MoveNext())
                return result;

            var prev = parts.Current;
            var sb = new StringBuilder();
            void Collect()
            {
                if (sb.Length == 0)
                    return;

                result.Add(new IdentifierPart(sb.ToString(), PartType.Abbreviation));
                sb.Clear();
            }

            IdentifierPart Process(IdentifierPart part)
            {
                if (prev.MaybeAbbr())
                {
                    sb.Append(prev.Value);
                }
                else
                {
                    Collect();
                    result.Add(prev);
                }

                return part;
            };

            while (parts.MoveNext())
            {
                prev = Process(parts.Current);
            }
            Process(prev);
            Collect();

            return result;
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
