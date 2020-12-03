namespace Identifier.SpellChecker
{
    public readonly struct IdentifierPart
    {
        public readonly string Value;
        public readonly PartType Type;

        public IdentifierPart(string part, PartType type)
        {
            Value = part;
            Type = type;
        }
    }
}
