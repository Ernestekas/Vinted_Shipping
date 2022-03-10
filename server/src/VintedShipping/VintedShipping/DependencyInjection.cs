using Microsoft.Extensions.DependencyInjection;
using VintedShipping.Interfaces;
using VintedShipping.Services;

namespace VintedShipping
{
    public static class DependencyInjection
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<IInputFileService, InputFileService>();
            services.AddTransient<ConsoleService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<IProviderService, ProviderService>();
        }
    }
}
