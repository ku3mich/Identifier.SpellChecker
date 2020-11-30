using Microsoft.Extensions.DependencyInjection;
using XUnit.Extensions.Essentials;

namespace Identifier.SpellChecker.Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services) => services.AddXUnitEssentials();
    }
}
