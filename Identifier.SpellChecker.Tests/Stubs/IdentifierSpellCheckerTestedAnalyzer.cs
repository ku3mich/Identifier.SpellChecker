using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SpellChecker;
using Xunit;
using Xunit.Abstractions;

namespace Identifier.SpellChecker.Tests
{
    public class IdentifierSpellCheckerTestedAnalyzer : IdentifierSpellCheckerAnalyzer
    {
        private readonly ITestOutputHelper TestOutputHelper;
        private readonly ISpellChecker SpellChecker;

        public IdentifierSpellCheckerTestedAnalyzer()
        {
            System.Collections.Generic.IReadOnlyList<Parameter> parameters = XunitContext.Context.Parameters;

            TestOutputHelper = XunitContext.Context.TestOutput;
            SpellChecker = (ISpellChecker)parameters[1].Value;
        }

        protected override void ConfigureSpeller(IServiceCollection services)
            => services.AddSingleton(SpellChecker);

        protected override void ConfigureLogger(IServiceCollection services)
            => services.AddLogging(b => b.SetMinimumLevel(LogLevel.Trace).AddXUnit(TestOutputHelper));
    }
}
