using Microsoft.Extensions.Hosting;
using Xunit.Extensions.Essentials;

namespace Identifier.SpellChecker.Tests
{
    public class Startup
    {
        public void ConfigureHost(IHostBuilder hostBuilder) => hostBuilder.ConfigureXunitEssentials();
    }
}
