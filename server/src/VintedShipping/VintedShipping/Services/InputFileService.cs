using System.IO;

namespace VintedShipping.Services
{
    public class InputFileService
    {
        private readonly string inputFile = "Data/input.txt";
        private readonly string providersFile = "Data/Providers.txt";

        public string[] ReadInputAsync()
        {
            return File.ReadAllLines(inputFile);
        }

        public string[] ReadProvidersAsync()
        {
            return File.ReadAllLines(providersFile);
        }
    }
}
