using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using VintedShipping.Interfaces;
using VintedShipping.Models;

namespace VintedShipping.Services
{
    public class ProviderService : IProviderService
    {
        private readonly IInputFileService _inputFileService;
        private readonly NumberFormatInfo _decimalCulture = new NumberFormatInfo { NumberDecimalSeparator = "." };

        public ProviderService(IInputFileService inputFileService)
        {
            _inputFileService = inputFileService;
        }

        public async Task<List<Provider>> GetProvidersAsync()
        {
            List<Provider> providers = new List<Provider>();
            string[] rawProvidersData = await _inputFileService.ReadProvidersAsync();

            foreach (var providerData in rawProvidersData)
            {
                string[] providerParameters = providerData.Split(' ');

                if (!ValidateProviderString(providerData))
                {
                    continue;
                }

                UpdateProviders(providerParameters, providers);
                
            }
            
            return providers;
        }

        private bool ValidateProviderString(string rawProviderData)
        {
            string[] splitData = rawProviderData.Split(' ');
            if (splitData.Length != 3)
            {
                return false;
            }

            try
            {
                var numberFormatInfo = new NumberFormatInfo { NumberDecimalSeparator = "." };
                decimal.Parse(splitData[2], numberFormatInfo);
            }
            catch
            {
                return false;
            }
            return true;
        }

        private void UpdateProviders(string[] providerParameters, List<Provider> providers)
        {
            switch(providers.FirstOrDefault(p => p.Code == providerParameters[0]) == null)
            {
                case true:
                    AddNewProvider(providerParameters, providers);
                    break;
                case false:
                    AddPackage(providerParameters, providers);
                    break;
            }
        }

        private void AddNewProvider(string[] providerParameters, List<Provider> providers)
        {
            var provider = new Provider()
            {
                Code = providerParameters[0]
            };

            provider.Packages.Add(new Package()
            {
                SizeAbbreviation = providerParameters[1],
                BasePrice = decimal.Parse(providerParameters[2], _decimalCulture)
            });

            providers.Add(provider);
        }

        private void AddPackage(string[] providerParameters, List<Provider> providers)
        {
            int index = providers.FindLastIndex(p => p.Code == providerParameters[0]);
            providers[index].Packages.Add(new Package
            {
                SizeAbbreviation = providerParameters[1],
                BasePrice = decimal.Parse(providerParameters[2], _decimalCulture)
            });
        }
    }
}
