using System.Threading.Tasks;

namespace VintedShipping.Interfaces
{
    public interface IInputFileService
    {
        Task<string[]> ReadInputAsync();
        Task<string[]> ReadProvidersAsync();
    }
}