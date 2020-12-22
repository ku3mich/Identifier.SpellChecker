using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace Identifier.SpellChecker.Tests
{
    public class SpellingTests
    {
        private readonly ITestOutputHelper TestOutputHelper;

        public SpellingTests(ITestOutputHelper testOutputHelper)
        {
            TestOutputHelper = testOutputHelper;
        }

        [InlineData("XmlUtility")]
        [Theory]
        public void Spelling(string identifier)
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(b => b.SetMinimumLevel(LogLevel.Trace).AddXUnit(TestOutputHelper));
            serviceCollection.AddSingleton(RealSpellChecker.Instance);
            serviceCollection.AddSingleton<IIdentifierSpeller, IdentifierSpeller>();

            using ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            IIdentifierSpeller speller = serviceProvider.GetRequiredService<IIdentifierSpeller>();
            speller.Check(identifier);
        }
    }
}
