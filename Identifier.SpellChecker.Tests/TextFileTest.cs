using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Identifier.SpellChecker.Tests
{
    public abstract class TextFileTest : XunitTest
    {
        private readonly TextFileProvider TextFileProvider;

        protected TextFileTest(ITestOutputHelper output, TextFileProvider provider) : base(output)
        {
            TextFileProvider = provider;
        }

        protected Task<string> ProvideTextAsync(string fname) => TextFileProvider.ProvideAsync(fname);
    }
}
