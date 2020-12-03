namespace Identifier.SpellChecker
{
    public interface IIdentifierSpeller
    {
        IdentifierCheckResult Check(string identifier);
    }
}
