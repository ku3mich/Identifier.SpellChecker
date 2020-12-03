using Xunit;

namespace SpellChecker.Tests
{
    public class CustomDictionaryTetsts
    {
        readonly ISpellChecker Checker;

        public CustomDictionaryTetsts(IPathResolver resolver)
        {
            Checker = new FileWordListChecker(resolver.Resolve("dictionary.spell"));
        }

        [Theory]
        [InlineData("Antlr")]
        [InlineData("Xml")]
        [InlineData("Xsd")]
        [InlineData("xsd")]
        public void Spelling(string s)
        {
            var result = Checker.Check(s);
            Assert.True(result);
        }
    }
}
