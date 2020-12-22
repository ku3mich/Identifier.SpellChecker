using System.Linq;
using Xunit;

namespace Identifier.SpellChecker.Tests
{
    public class CamelCaseSplitTests
    {
        [Fact]
        public void Null()
        {
            Assert.Empty(((string)null).SplitCamelCase());
        }


        [Fact]
        public void AnalyzeEmpty()
        {
            Assert.Empty(((string)null).SplitCamelCase().Analyze());
        }

        [Theory]
        [InlineData("A", "A")]
        [InlineData("IInterface", "I")]
        [InlineData("InterfaceABBR", "ABBR")]
        [InlineData("XCode", "X")]
        public void Abbreviation(string testCase, string expected)
        {
            IdentifierPart[] parts = testCase
                .SplitCamelCase()
                .Analyze()
                .Where(s => s.Type == PartType.Abbreviation)
                .ToArray();

            Assert.Single(parts);
            Assert.Equal(expected, parts[0].Value);
        }

        [Fact]
        public void LowerCaseWord()
        {
            IdentifierPart[] results = "word".SplitCamelCase().ToArray();
            Assert.Single(results);
            IdentifierPart result = results[0];
            Assert.Equal("word", result.Value);
            Assert.Equal(PartType.Word, result.Type);
        }

        [Fact]
        public void CamelCase()
        {
            IdentifierPart[] results = "wordBreak".SplitCamelCase().ToArray();
            Assert.Equal(2, results.Length);
            Assert.All(results, s => Assert.Equal(PartType.Word, s.Type));
            Assert.Equal("word", results[0].Value);
            Assert.Equal("Break", results[1].Value);
        }

        [Fact]
        public void CamelCaseElse()
        {
            IdentifierPart[] results = "word1Break".SplitCamelCase().ToArray();
            Assert.Equal(3, results.Length);
            Assert.Contains(new IdentifierPart("1", PartType.Else), results);
        }
    }
}
