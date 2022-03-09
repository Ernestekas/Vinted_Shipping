using Microsoft.Extensions.DependencyInjection;
using VintedShipping.Services;

namespace VintedShipping
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();

            services.ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();
            var consoleService = serviceProvider.GetService<ConsoleService>();

            consoleService.Run();
        }
    }
}
