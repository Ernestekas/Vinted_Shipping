using System.IO;
using System.Threading.Tasks;
using VintedShipping.Interfaces;

namespace VintedShipping.Services
{
    public class InputFileService : IInputFileService
    {
        private readonly string inputFile = "Data/input.txt";
        private readonly string providersFile = "Data/Providers.txt";

        public async Task<string[]> ReadInputAsync()
        {
            return await File.ReadAllLinesAsync(inputFile);
        }

        public async Task<string[]> ReadProvidersAsync()
        {
            return await File.ReadAllLinesAsync(providersFile);
        }
    }
}
