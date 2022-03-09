using System.Collections.Generic;
using System.Threading.Tasks;
using VintedShipping.Models;

namespace VintedShipping.Interfaces
{
    public interface IProviderService
    {
        Task<List<Provider>> GetProvidersAsync();
    }
}