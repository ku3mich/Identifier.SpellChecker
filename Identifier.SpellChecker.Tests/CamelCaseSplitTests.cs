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
        public void LowerCaseWord()
        {
            var results = "word".SplitCamelCase().ToArray();
            Assert.Single(results);
            var result = results[0];
            Assert.Equal("word", result.Part);
            Assert.Equal(PartType.Word, result.Type);
        }

        [Fact]
        public void CamelCase()
        {
            var results = "wordBreak".SplitCamelCase().ToArray();
            Assert.Equal(2, results.Length);
            Assert.All(results, s => Assert.Equal(PartType.Word, s.Type));
            Assert.Equal("word", results[0].Part);
            Assert.Equal("Break", results[1].Part);
        }

        [Fact]
        public void CamelCaseElse()
        {
            var results = "word1Break".SplitCamelCase().ToArray();
            Assert.Equal(3, results.Length);
            Assert.Contains(new IdentifierPart("1", PartType.Else), results);
        }
    }
}
