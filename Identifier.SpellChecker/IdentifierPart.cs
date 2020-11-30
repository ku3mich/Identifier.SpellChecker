namespace Identifier.SpellChecker
{
    public readonly struct IdentifierPart
    {
        public readonly string Part;
        public readonly PartType Type;

        public IdentifierPart(string part, PartType type)
        {
            Part = part;
            Type = type;
        }
    }
}
