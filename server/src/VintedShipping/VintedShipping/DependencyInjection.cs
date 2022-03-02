using Microsoft.Extensions.DependencyInjection;
using VintedShipping.Services;

namespace VintedShipping
{
    public static class DependencyInjection
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<InputFileService>();
            services.AddTransient<ConsoleService>();
            services.AddTransient<TransactionService>();
        }
    }
}
