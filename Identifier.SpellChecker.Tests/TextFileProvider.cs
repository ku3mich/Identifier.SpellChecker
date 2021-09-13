using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Identifier.SpellChecker.Tests
{
    public class TextFileProvider
    {
        private readonly IPathResolver Resolver;

        public TextFileProvider(IPathResolver resolver)
        {
            Resolver = resolver;
        }

        public Task<string> ProvideAsync(string fname)
            => File.ReadAllTextAsync(Resolver.Resolve(fname));
    }
}
