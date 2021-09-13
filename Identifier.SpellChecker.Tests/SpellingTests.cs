using Xunit;
using Xunit.Abstractions;

namespace Identifier.SpellChecker.Tests
{
    public class SpellingTests : XunitTest
    {
        private readonly IIdentifierSpeller Speller;

        public SpellingTests(ITestOutputHelper helper, IdentifierSpeller speller) : base(helper)
        {
            Speller = speller;
        }

        [Fact]
        public void Spelling()
        {
            var result = Speller.Check("XmlUtility");
            Assert.False(result.IsCorrect);
            Assert.Equal(2, result.Parts.Length);
            Assert.False(result.Parts[0].IsCorrect);
            Assert.True(result.Parts[1].IsCorrect);
        }
    }
}
