using Microsoft.Extensions.DependencyInjection;
using System;

namespace VintedShipping
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();

            services.ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();
        }
    }
}
