namespace Identifier.SpellChecker
{
    static class BoolExtensions
    {
        public static string IsCorrect(this bool b) => b ? "OK" : "ERR";
    }
}
