namespace Identifier.SpellChecker
{
    public readonly struct CheckedPart
    {
        public readonly IdentifierPart Part;
        public readonly bool IsCorrect;
        public readonly string[] Suggestions;

        public CheckedPart(IdentifierPart part, bool isCorrect, string[] suggestions)
        {
            Part = part;
            IsCorrect = isCorrect;
            Suggestions = suggestions ?? new string[] { };
        }
    }
}
