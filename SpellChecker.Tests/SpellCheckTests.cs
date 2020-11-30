using System;
using System.Linq;
using Xunit;

namespace SpellChecker.Tests
{
    public class SpellCheckTests
    {
        [Fact]
        public void SimpleCheck_Correct()
        {
            var result = Spell.Checker.Check("father");
            Assert.True(result);
        }

        [Fact]
        public void SimpleCheck_Incorrect()
        {
            var result = Spell.Checker.Check("fathe");
            Assert.False(result);
        }

        [Fact]
        public void SimpleCheck_Details()
        {
            var result = Spell.Checker.CheckDetails("fathe");
            Assert.False(result.Correct);
        }

        [Fact]
        public void SimpleCheck_Suggest()
        {
            var result = Spell.Checker.Suggest("fathe");
            Assert.NotEmpty(result);
        }
    }
}
