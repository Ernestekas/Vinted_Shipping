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

                providers.Add(FormProvider(providerParameters, providers));
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

        private Provider FormProvider(string[] providerParameters, List<Provider> providers)
        {
            Provider provider = new Provider();

            if (providers.FirstOrDefault(p => p.Code == providerParameters[0]) == null)
            {
                provider = new Provider()
                {
                    Code = providerParameters[0]
                };
            }
            else
            {
                provider = providers.FirstOrDefault(p => p.Code == providerParameters[0]);
            }

            var numberFormatInfo = new NumberFormatInfo { NumberDecimalSeparator = "." };

            provider.Packages.Add(new Package()
            {
                SizeAbbreviation = providerParameters[1],
                BasePrice = decimal.Parse(providerParameters[2], numberFormatInfo)
            });
            return provider;
        }
    }
}
