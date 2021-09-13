using Xunit;

namespace SpellChecker.Tests
{
    public class SpellCheckTests
    {
        [Fact]
        public void SimpleCheck_Correct()
        {
            bool result = Spell.Checker.Check("father");
            Assert.True(result);
        }

        [Fact]
        public void SimpleCheck_Incorrect()
        {
            bool result = Spell.Checker.Check("fathe");
            Assert.False(result);
        }

        [Fact]
        public void SimpleCheck_Details()
        {
            WeCantSpell.Hunspell.SpellCheckResult result = Spell.Checker.CheckDetails("fathe");
            Assert.False(result.Correct);
        }

        [Fact]
        public void SimpleCheck_Suggest()
        {
            System.Collections.Generic.IEnumerable<string> result = Spell.Checker.Suggest("fathe");
            Assert.NotEmpty(result);
        }
    }
}
