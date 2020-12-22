using Microsoft.Extensions.Hosting;
using Xunit.Extensions.Essentials;

namespace SpellChecker.Tests
{
    public class Startup
    {
        public void ConfigureHost(IHostBuilder hostBuilder) => hostBuilder.ConfigureXunitEssentials();
    }
}
