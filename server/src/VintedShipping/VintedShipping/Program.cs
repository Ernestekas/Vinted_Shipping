using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using VintedShipping.Services;

namespace VintedShipping
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();

            services.ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();
            var consoleService = serviceProvider.GetService<ConsoleService>();

            await consoleService.Run();
        }
    }
}
