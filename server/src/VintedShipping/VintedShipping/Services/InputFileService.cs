using System.IO;
using System.Threading.Tasks;

namespace VintedShipping.Services
{
    public class InputFileService
    {
        private readonly string inputFile = "input.txt";
        private readonly string providersFile = "Providers.txt";

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
